using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    class Scanner
    {
        private bool FrameRequested;
        private bool isBusy;
        private List<Slice> Slices;

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

        private void Camera_NewFrame(object sender, NewFrameEventArgs e)
        {
            if (FrameRequested)
            {
                FrameRequested = false;
                HandleNewFrame(e.Frame);
            }
        }

        private void HandleNewFrame(System.Drawing.Bitmap frame)
        {
            var unmanaged = UnmanagedImage.FromManagedImage(frame);
            Grayscale grayscaleFilter = new Grayscale(1, 0, 0);
            var grayscaleUnm = grayscaleFilter.Apply(unmanaged);

            Vector2[] points = PointDetector.FindHighlitedPoints(grayscaleUnm.ImageData, frame.Width, frame.Height);
            var points3Enumerable = from point in points
                                    select PointScanner.ConvertTo3D(point, frame.Width, frame.Height) into point3
                                    where Cylinder.Contains(point3)
                                    select point3;
            Vector3[] points3 = points3Enumerable.ToArray();

            Slices.Add(new Slice(-Slices.Count * PlatformStep, points3));
        }

        private Mesh BuildMesh()
        {
            Mesh result = new Mesh();

            result.TextureCoordinates.Add(new Vector2(0, 0));

            int[] slicePointsOffset = new int[Slices.Count];
            for (int i = 0; i < Slices.Count; i++)
            {
                var slice = Slices[i];
                slicePointsOffset[i] = result.Vertices.Count;

                Quaternion rotation = Quaternion.CreateFromYawPitchRoll((float)slice.Rotation, 0, 0);
                foreach (var p in slice.Points)
                {
                    var rp = Vector3.Transform(p, rotation);
                    var sp = Vector3.Multiply(1e-2f, rp);
                    result.Vertices.Add(sp);
                }
            }

            for (int i = 0; i < Slices.Count; i++)
            {
                int sliceAInd = i;
                int sliceBInd = (i + 1) % Slices.Count;

                int lenA = Slices[sliceAInd].Points.Length;
                int lenB = Slices[sliceBInd].Points.Length;
                int pointsAOffset = slicePointsOffset[sliceAInd];
                int pointsBOffset = slicePointsOffset[sliceBInd];

                float max = Math.Max(lenA, lenB);
                float accA = 0, accB = 0;
                float stepA = lenA / max, stepB = lenB / max;
                int indA = 0, indB = 0;
                int pointA = pointsAOffset, pointB = pointsBOffset;
                while (indA + 1 < lenA || indB + 1 < lenB)
                {
                    accA += stepA; accB += stepB;
                    if (indA + 1 < lenA && accA >= 1)
                    {
                        accA--;
                        int newPoint = pointsAOffset + ++indA;
                        result.Faces.Add(new List<VertexInfo>()
                        {
                            new VertexInfo(pointA, 0),
                            new VertexInfo(pointB, 0),
                            new VertexInfo(newPoint, 0)
                        });
                        pointA = newPoint;
                    }
                    if (indB + 1 < lenB && accB >= 1)
                    {
                        accB--;
                        int newPoint = pointsBOffset + ++indB;
                        result.Faces.Add(new List<VertexInfo>()
                        {
                            new VertexInfo(pointA, 0),
                            new VertexInfo(pointB, 0),
                            new VertexInfo(newPoint, 0)
                        });
                        pointB = newPoint;
                    }
                }
            }
            return result;
        }
        
        public async Task<Mesh> Scan()
        {
            if (isBusy) throw new InvalidOperationException();
            isBusy = true;

            Slices = new List<Slice>();
            int scansCount = (int)Math.Floor(Math.PI * 2 / PlatformStep);
            await Device.Start();
            for (int i = 0; i < scansCount; i++)
            {
                FrameRequested = true;
                while (FrameRequested) ;
                await Device.Rotate(PlatformStep);
            }
            await Device.Stop();
            var result = BuildMesh();

            Slices = null;
            isBusy = false;
            return result;
        }

        private class Slice
        {
            public double Rotation { get; private set; }
            public Vector3[] Points { get; private set; }

            public Slice(double rotation, Vector3[] points)
            {
                Rotation = rotation;
                Points = points;
            }
        }
    }
}
