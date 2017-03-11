using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    class VertexInfo
    {
        public int VertexIndex { get; private set; }
        public int TextureIndex { get; private set; }

        public VertexInfo(int vertexIndex, int textureIndex)
        {
            VertexIndex = vertexIndex;
            TextureIndex = textureIndex;
        }
    }

    class FaceInfo : IEnumerable<VertexInfo>
    {
        private VertexInfo[] Vertices;

        public VertexInfo this[int index] => Vertices[index];

        public FaceInfo(IEnumerable<VertexInfo> vertices)
        {
            this.Vertices = vertices.ToArray();
        }

        public IEnumerator<VertexInfo> GetEnumerator()
        {
            return ((IEnumerable<VertexInfo>)Vertices).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<VertexInfo>)Vertices).GetEnumerator();
        }
    }

    class Mesh
    {
        public List<Vector3> Vertices { get; } = new List<Vector3>();
        public List<Vector2> TextureCoordinates { get; } = new List<Vector2>();
        public List<FaceInfo> Faces { get; } = new List<FaceInfo>();
        public Bitmap Texture { get; set; } = null;
        private string DToS(double val) => val.ToString(NumberFormatInfo.InvariantInfo);
        public void WriteToFile(string dir, string name)
        {
            string dirName = $@"{dir}\{name}\";
            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);
            string relTexName = $"{name}.png";
            string relMtlName = $"{name}.mtl";
            string relObjName = $"{name}.obj";
            string texName = dirName + relTexName;
            string mtlName = dirName + relMtlName;
            string objName = dirName + relObjName;
            if (Texture != null)
            {
                Texture.Save(texName, ImageFormat.Png);
            }
            using (var writer = new StreamWriter(mtlName))
            {
                writer.WriteLine($"newmtl {name}_Material");
                writer.WriteLine($"Ka 0.000000 0.000000 0.000000");
                writer.WriteLine($"Kd 1.000000 1.000000 1.000000");
                writer.WriteLine($"Ks 0.000000 0.000000 0.000000");
                //writer.WriteLine($"Ni 1.000000");
                //writer.WriteLine($"d 1.000000");
                //writer.WriteLine($"illum 1");
                if (Texture != null)
                    writer.WriteLine($"map_Kd {relTexName}");
            }
            using (var writer = new StreamWriter(objName))
            {
                writer.WriteLine($"o {name}");
                writer.WriteLine($"mtllib {relMtlName}");
                writer.WriteLine($"usemtl {name}_Material");
                foreach (var v in Vertices)
                    writer.WriteLine($"v {DToS(v.X)} {DToS(v.Y)} {DToS(v.Z)}");
                foreach (var vt in TextureCoordinates)
                    writer.WriteLine($"vt {DToS(vt.X)} {DToS(vt.Y)}");
                foreach (var f in Faces)
                {
                    writer.Write("f ");
                    writer.WriteLine(
                        string.Join(" ",
                        f.Select(v => $"{v.VertexIndex + 1}/{v.TextureIndex + 1}")));
                }
            }
        }
    }
}
