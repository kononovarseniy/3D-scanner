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

namespace Scan3D.GraphicsUtils
{
    public class VertexInfo
    {
        public int VertexIndex { get; private set; }
        public int TextureIndex { get; private set; }

        public VertexInfo(int vertexIndex, int textureIndex)
        {
            VertexIndex = vertexIndex;
            TextureIndex = textureIndex;
        }
    }

    public class FaceInfo : IEnumerable<VertexInfo>
    {
        private VertexInfo[] Vertices;

        public VertexInfo this[int index] => Vertices[index];

        public int VerticesCount => Vertices.Length;

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

    public class Mesh
    {
        public List<Vector3> Vertices { get; } = new List<Vector3>();
        public List<Vector2> TextureCoordinates { get; } = new List<Vector2>();
        public List<FaceInfo> Faces { get; } = new List<FaceInfo>();
        public Bitmap Texture { get; set; } = null;
        private static string ToString(double val) => val.ToString(NumberFormatInfo.InvariantInfo);
        private static float ParseFloat(string s) => float.Parse(s, NumberFormatInfo.InvariantInfo);
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
                    writer.WriteLine($"v {ToString(v.X)} {ToString(v.Y)} {ToString(v.Z)}");
                foreach (var vt in TextureCoordinates)
                    writer.WriteLine($"vt {ToString(vt.X)} {ToString(vt.Y)}");
                foreach (var f in Faces)
                {
                    writer.Write("f ");
                    writer.WriteLine(
                        string.Join(" ",
                        f.Select(v => $"{v.VertexIndex + 1}/{v.TextureIndex + 1}")));
                }
            }
        }
        /// <summary>
        /// Simple wavefront obj parser. Only for debug purposes.
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static Mesh FromFile(string filename)
        {
            string dir = Path.GetDirectoryName(filename);
            string mtl = null;

            List<Vector3> vertices = new List<Vector3>();
            List<Vector2> texVertices = new List<Vector2>();
            List<FaceInfo> faces = new List<FaceInfo>();

            foreach (var line in File.ReadAllLines(filename))
            {
                string[] parts = line.ToLower().Split(' ');
                if (parts[0] == "v")
                {
                    float x = ParseFloat(parts[1]);
                    float y = ParseFloat(parts[2]);
                    float z = ParseFloat(parts[3]);
                    vertices.Add(new Vector3(x, y, z));
                }
                if (parts[0] == "vt")
                {
                    float x = ParseFloat(parts[1]);
                    float y = ParseFloat(parts[2]);
                    texVertices.Add(new Vector2(x, y));
                }
                if (parts[0] == "f")
                {
                    VertexInfo[] faceVertices = new VertexInfo[3];
                    for (int i = 0; i < 3; i++)
                    {
                        int[] indices = parts[i + 1].Split('/').Select(s => int.Parse(s)).ToArray();
                        int vind = indices.Length >= 1 ? indices[0] - 1 : 0;
                        int tind = indices.Length >= 2 ? indices[1] - 1 : 0;
                        faceVertices[i] = new VertexInfo(vind, tind);
                    }
                    faces.Add(new FaceInfo(faceVertices));
                }
            }
            var res = new Mesh();
            res.Vertices.AddRange(vertices);
            res.TextureCoordinates.AddRange(texVertices);
            res.Faces.AddRange(faces);
            return res;
        }
    }
}
