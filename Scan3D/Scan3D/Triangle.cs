using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    /// <summary>
    /// Executes mapping of triangle of texture to other triangle.
    /// </summary>
    class Triangle
    {
        private Vector2[] vertices;
        private Vector2 P;
        private Vector2 A;
        private Vector2 B;

        public Triangle(Vector2[] vertices)
        {
            P = vertices[0];
            A = vertices[1] - vertices[0];
            B = vertices[2] - vertices[0];

            this.vertices = new Vector2[vertices.Length];
            Array.Copy(vertices, this.vertices, vertices.Length);
            Array.Sort(this.vertices, (a, b) => a.Y.CompareTo(b.Y));
        }

        public Vector2 MapPoint(float x, float y, Triangle triangle)
        {
            float denom = (A.X * B.Y - A.Y * B.X);
            float a = -(B.X * y - B.Y * x + P.X * B.Y - P.Y * B.X) / denom;
            float b = +(A.X * y - A.Y * x + P.X * A.Y - P.Y * A.X) / denom;
            return triangle.P + Vector2.Multiply(a, triangle.A) + Vector2.Multiply(b, triangle.B);
        }
        public Vector2 MapPoint(Vector2 point, Triangle triangle) => MapPoint(point.X, point.Y, triangle);

        private static unsafe void CopyLine(BitmapData srcData, BitmapData dstData, int y, int left, int right, Triangle src, Triangle dst)
        {
            //if (y < 0 || y >= dstData.Height) return;
            if (right < left)
            {
                int tmp = right;
                right = left;
                left = tmp;
            }
            if (left < 0) left = 0;
            if (right >= dstData.Width) right = dstData.Width - 1;
            byte* dst0 = (byte*)dstData.Scan0;
            byte* src0 = (byte*)srcData.Scan0;
            int lineOffset = y * dstData.Stride;
            int maxSrc = srcData.Stride * srcData.Height - 3;
            for (int x = left; x <= right; x++)
            {
                var p = dst.MapPoint(x, y, src);
                int dstOffset = lineOffset + x * 3;
                int srcOffset = (int)p.Y * srcData.Stride + (int)p.X * 3;
                //if (srcOffset > maxSrc || srcOffset < 0) continue;
                dst0[dstOffset + 0] = src0[srcOffset + 0];
                dst0[dstOffset + 1] = src0[srcOffset + 1];
                dst0[dstOffset + 2] = src0[srcOffset + 2];
            }
        }

        private static void CopyTriangle(BitmapData srcData, Triangle srcTriangle, BitmapData dstData, Triangle dstTriangle)
        {
            float y0 = dstTriangle.vertices[0].Y, x0 = dstTriangle.vertices[0].X;
            float y1 = dstTriangle.vertices[1].Y, x1 = dstTriangle.vertices[1].X;
            float y2 = dstTriangle.vertices[2].Y, x2 = dstTriangle.vertices[2].X;

            float dx01 = 0;
            if (y1 != y0) dx01 = (x1 - x0) / (y1 - y0);

            float dx12 = 0;
            if (y2 != y1) dx12 = (x2 - x1) / (y2 - y1);

            float dx02 = 0;
            if (y2 != y0) dx02 = (x2 - x0) / (y2 - y0);

            for (int y = (int)Math.Floor(y0); y <= Math.Ceiling(y1); y++)
            {
                int left = (int)Math.Floor(x0 + dx01 * (y - y0));
                int right = (int)Math.Ceiling(x0 + dx02 * (y - y0));
                CopyLine(srcData, dstData, y, left, right, srcTriangle, dstTriangle);
            }
            for (int y = (int)Math.Floor(y1); y <= Math.Ceiling(y2); y++)
            {
                int left = (int)Math.Floor(x1 + dx12 * (y - y1));
                int right = (int)Math.Ceiling(x2 + dx02 * (y - y2));
                CopyLine(srcData, dstData, y, left, right, srcTriangle, dstTriangle);
            }
        }

        public void MapTexture(BitmapData srcData, BitmapData dstData, Triangle dstTriangle)
        {
            CopyTriangle(srcData, this, dstData, dstTriangle);
        }
    }
}
