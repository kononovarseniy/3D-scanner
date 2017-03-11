using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    class Scanner
    {
        // Model data
        private int TotalSlices;
        private List<Vector3> Vertices;
        private List<Vector2> TexVertices;
        private List<SliceInfo> Slices;
        private List<FaceInfo[]> Stripes;
        private Bitmap Texture;
        private BitmapData TextureData;
        private List<int> SliceToTextureMap;

        // State
        private bool FrameRequested;
        private bool ScanningTexture = false;
        private bool isBusy;
        private int TextureIndex;
        private double ModelRotation;

        // Configuration
        public DeviceController Device { get; private set; }
        public double PlatformStep { get; private set; }
        public VideoCaptureDevice Camera { get; private set; }
        public HighlitedPointDetector PointDetector { get; private set; }
        public PointScanner PointScanner { get; private set; }
        public Cylinder Cylinder { get; private set; }
        

        public Scanner(DeviceController device, double platformStep, VideoCaptureDevice camera, HighlitedPointDetector pointDetector, PointScanner pointScanner, Cylinder cylinder)
        {
            Device = device;
            PlatformStep = platformStep;
            Camera = camera;
            PointDetector = pointDetector;
            PointScanner = pointScanner;
            Cylinder = cylinder;

            FrameRequested = false;
            camera.NewFrame += Camera_NewFrame;
        }
        

        private void ResetModelData()
        {
            Vertices = new List<Vector3>();
            TexVertices = new List<Vector2>();
            Slices = new List<SliceInfo>();
            Stripes = new List<FaceInfo[]>();
            SliceToTextureMap = new List<int>();
            Texture = new Bitmap(Camera.FrameSize.Width * 10, Camera.FrameSize.Height * 10);
        }

        private int AddTextureVertices(int slice, int count)
        {
            if (slice < SliceToTextureMap.Count)
                return SliceToTextureMap[slice];

            int firstOffset = TexVertices.Count;
            SliceToTextureMap.Add(firstOffset);

            float x = 1f / TotalSlices * slice;
            for (int i = 0; i < count; i++)
            {
                float y = 1 - 1f / (count - 1) * i;
                TexVertices.Add(new Vector2(x, y));
            }
            return firstOffset;
        }

        private void AddStripe(int left, int right)
        {
            SliceInfo a = Slices[left];
            SliceInfo b = Slices[right % TotalSlices];
            int lenA = a.VerticesCount;
            int lenB = b.VerticesCount;

            int aVerticesOffset = a.FirstVertex;
            int bVerticesOffset = b.FirstVertex;
            int aTextureOffset = AddTextureVertices(left, lenA);
            int bTextureOffset = AddTextureVertices(right, lenB);

            float max = Math.Max(lenA, lenB);
            float accA = 0, accB = 0;
            float stepA = lenA / max, stepB = lenB / max;
            int indA = 0, indB = 0;
            int vertexA = aVerticesOffset, vertexB = bVerticesOffset;
            int textureA = aTextureOffset, textureB = bTextureOffset;

            List<FaceInfo> faces = new List<FaceInfo>();
            while (indA + 1 < lenA || indB + 1 < lenB)
            {
                accA += stepA; accB += stepB;
                if (indA + 1 < lenA && accA >= 1)
                {
                    accA--;
                    indA++;
                    int newVertex = aVerticesOffset + indA;
                    int newTexture = aTextureOffset + indA;
                    faces.Add(new FaceInfo(new []
                        {
                            new VertexInfo(vertexA, textureA),
                            new VertexInfo(vertexB, textureB),
                            new VertexInfo(newVertex, newTexture)
                        }));
                    vertexA = newVertex;
                    textureA = newTexture;
                }
                if (indB + 1 < lenB && accB >= 1)
                {
                    accB--;
                    indB++;
                    int newVertex = bVerticesOffset + indB;
                    int newTexture = bTextureOffset + indB;
                    faces.Add(new FaceInfo(new[]
                        {
                            new VertexInfo(vertexA, textureA),
                            new VertexInfo(vertexB, textureB),
                            new VertexInfo(newVertex, newTexture)
                        }));
                    vertexB = newVertex;
                    textureB = newTexture;
                }
            }

            Stripes.Add(faces.ToArray());
        }

        private void AddSlice(Vector3[] vertices)
        {
            int sliceIndex = Slices.Count;
            Slices.Add(new SliceInfo(this.Vertices.Count, vertices.Length));
            Vertices.AddRange(vertices);
            if (sliceIndex > 0) AddStripe(sliceIndex - 1, sliceIndex);
            if (sliceIndex == TotalSlices - 1) AddStripe(sliceIndex, TotalSlices);
        }

        private void CopyTexture(Bitmap frame, int stripeIndex, double modelRotation)
        {
            FaceInfo[] faces = Stripes[stripeIndex];

            int w = Texture.Width - 1;
            int h = Texture.Height - 1;
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll((float)modelRotation, 0, 0);
            BitmapData data = frame.LockBits(new Rectangle(Point.Empty, frame.Size), ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            foreach (var face in faces)
            {
                Triangle src = Triangle.FromVertices(
                    from info in face
                    select Vector3.Transform(Vertices[info.VertexIndex], rotation) into vertex
                    select PointScanner.ConvertTo2D(vertex, frame.Width, frame.Height));
                Triangle dst = Triangle.FromVertices(
                    from info in face
                    select TexVertices[info.TextureIndex] into point
                    select new Vector2(w * point.X, h * (1 - point.Y)));
                src.MapTexture(data, TextureData, dst);
            }

            frame.UnlockBits(data);
        }

        private Mesh BuildModel()
        {
            Mesh result = new Mesh();
            result.Texture = Texture;
            result.Vertices.AddRange(Vertices);
            result.TextureCoordinates.AddRange(TexVertices);
            Stripes.ForEach(stripe => result.Faces.AddRange(stripe));
            return result;
        }


        private void HandleNewFrame(Bitmap frame)
        {
            var unmanaged = UnmanagedImage.FromManagedImage(frame);
            Grayscale grayscaleFilter = new Grayscale(1, 0, 0);
            var grayscaleUnm = grayscaleFilter.Apply(unmanaged);

            float rotationAngle = (float)(-Slices.Count * PlatformStep);
            Quaternion rotation = Quaternion.CreateFromYawPitchRoll(rotationAngle, 0, 0);

            Vector2[] points = PointDetector.FindHighlitedPoints(grayscaleUnm.ImageData, frame.Width, frame.Height);
            var sliceVertices = from point in points
                                select PointScanner.ConvertTo3D(point, frame.Width, frame.Height) into vertex
                                where Cylinder.Contains(vertex)
                                select Vector3.Transform(vertex, rotation);

            AddSlice(sliceVertices.ToArray());
        }

        private void HandleNewTextureFrame(Bitmap frame)
        {
            CopyTexture(frame, (TextureIndex++) % TotalSlices, ModelRotation);
            ModelRotation += PlatformStep;
        }
                
        private void Camera_NewFrame(object sender, NewFrameEventArgs e)
        {
            if (FrameRequested)
            {
                FrameRequested = false;
                if (ScanningTexture) HandleNewTextureFrame(e.Frame);
                else HandleNewFrame(e.Frame);
            }
        }

        private async Task DoScanning(int scansCount)
        {
            for (int i = 0; i < scansCount; i++)
            {
                FrameRequested = true;
                while (FrameRequested) ;
                await Device.Rotate(PlatformStep);
            }
        }


        public async Task<Mesh> Scan()
        {
            if (isBusy) throw new InvalidOperationException();
            isBusy = true;
            
            TotalSlices = (int)Math.Floor(Math.PI * 2 / PlatformStep);

            ResetModelData();
            TextureData = Texture.LockBits(new Rectangle(Point.Empty, Texture.Size), ImageLockMode.WriteOnly, PixelFormat.Format24bppRgb);

            // TODO: turn on laser
            await Device.Start();

            // Scanning of surface
            ScanningTexture = false;
            await DoScanning(TotalSlices);
            // TODO: turn off laser

            // TODO: turn on light
            await Task.Delay(10000);

            // Scanning of texture
            ScanningTexture = true;
            TextureIndex = 0;
            ModelRotation = 0;
            await DoScanning(TotalSlices);
            // TODO: turn off light

            await Device.Stop();

            Texture.UnlockBits(TextureData);

            Mesh result = BuildModel();

            Slices = null;
            isBusy = false;
            return result;
        }

        private struct SliceInfo
        {
            public int FirstVertex;
            public int VerticesCount;

            public SliceInfo(int first, int count)
            {
                FirstVertex = first;
                VerticesCount = count;
            }
        }
    }
}
