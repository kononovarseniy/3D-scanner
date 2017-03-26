using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Scan3D.GraphicsUtils;

namespace Scan3D.GraphicsUtils
{
    public partial class MeshRenderer : GLControl
    {
        private bool isLoaded = false;
        private Bitmap previousTexture = null;
        private GLTexture glTexture = null;

        public Mesh Mesh { get; set; }
        public float Scale { get; set; } = 1;
        public System.Numerics.Quaternion Rotation { get; set; } = System.Numerics.Quaternion.Identity;
        public System.Numerics.Vector3 Translation { get; set; } = System.Numerics.Vector3.Zero;

        public MeshRenderer()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            isLoaded = true;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            
            UpdateTexture();
            if (!isLoaded) return;
            if (Mesh == null) return;

            GL.ClearColor(Color.SkyBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Viewport(Point.Empty, this.Size);

            Matrix4 lookAt = Matrix4.LookAt(
                0, 0, 160,
                0, 0, 0,
                0, 1, 0);
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(
                fovy: (float)(60 * Math.PI / 180),
                aspect: (float)Width / Height,
                zNear: 20,
                zFar: 500);
            Matrix4 projection = lookAt * perspective;
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            Matrix4 scaleMatrix = Matrix4.Scale(Scale);
            Matrix4 rotationMatrix = Matrix4.Rotate(new Quaternion(Rotation.X, Rotation.Y, Rotation.Z, Rotation.W));
            Matrix4 translationMatrix = Matrix4.Translation(Translation.X, Translation.Y, Translation.Z);

            Matrix4 scaleAndRotation = scaleMatrix * rotationMatrix;

            Matrix4 mainAxisModelview = scaleAndRotation;
            Matrix4 modelview = translationMatrix * scaleAndRotation;

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref mainAxisModelview);
            DrawAxis();

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref modelview);
            DrawAxis();
            DrawMesh();

            SwapBuffers();
        }


        private void UpdateTexture()
        {
            Bitmap texture = Mesh?.Texture;
            if (texture != previousTexture)
            {
                glTexture?.Dispose();
                previousTexture = texture;
                if (texture != null)
                    glTexture = GLTexture.FromBitmap(texture);
                else
                    glTexture = null;
            }
        }

        private void DrawAxis()
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(50, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 50, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 50);
            GL.End();
        }

        private void DrawMesh()
        {
            if (glTexture != null)
            {
                GL.Enable(EnableCap.Texture2D);
                GLTexture.Bind(glTexture);
            }
            GL.Begin(PrimitiveType.Triangles);

            foreach (var face in Mesh.Faces)
            {
                foreach (var vertexInfo in face)
                {
                    var texture = Mesh.TextureCoordinates[vertexInfo.TextureIndex];
                    var vertex = Mesh.Vertices[vertexInfo.VertexIndex];
                    float u = texture.X;
                    float v = 1 - texture.Y;
                    float x = vertex.X / 2;
                    float y = vertex.Y / 2;
                    float z = vertex.Z / 2;
                    GL.TexCoord2(u, v);
                    GL.Vertex3(x, y, z);
                }
            }

            GL.End();
            GL.Disable(EnableCap.Texture2D);
        }
    }
}
