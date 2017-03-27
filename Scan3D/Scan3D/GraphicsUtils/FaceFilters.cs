using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D.GraphicsUtils
{
    public class ThinFaceFilter : IFaceFilter
    {
        private const double DefaultAngle = 5 * Math.PI / 180;
        private double maxCos;
        private double _minAngle;
        public double MinAngle
        {
            get
            {
                return _minAngle;
            }
            set
            {
                _minAngle = value;
                maxCos = Math.Cos(MinAngle);
            }
        }

        public ThinFaceFilter(double minAngle = DefaultAngle)
        {
            MinAngle = minAngle;
        }

        public bool Check(Vector3[] vertices)
        {
            float[] sidesSquared = new float[3];
            for (int i = 0; i < 3; i++)
            {
                int j = (i + 1) % 3;
                sidesSquared[i] = (vertices[i] - vertices[j]).LengthSquared();
            }
            for (int i = 0; i < 3; i++)
            {
                float aSqr = sidesSquared[i];
                float bSqr = sidesSquared[(i + 1) % 3];
                float cSqr = sidesSquared[(i + 2) % 3];
                // cos = (a^2 * b^2 - c^2) / 2ab
                double cos = (aSqr + bSqr - cSqr) / (2 * Math.Sqrt(aSqr * bSqr));
                if (cos > maxCos) return false;
            }
            return true;
        }
    }

    public class LargeFaceFilter : IFaceFilter
    {
        private const double DefaultMaxLength = 5;
        private double maxLengthSquared;
        private double _maxLength;
        public double MaxLength
        {
            get
            {
                return _maxLength;
            }
            set
            {
                _maxLength = value;
                maxLengthSquared = value * value;
            }
        }

        public LargeFaceFilter(double maxLength = DefaultMaxLength)
        {
            MaxLength = maxLength;
        }

        public bool Check(Vector3[] vertices)
        {
            float[] sidesSquared = new float[3];
            for (int i = 0; i < 3; i++)
            {
                int j = (i + 1) % 3;
                sidesSquared[i] = (vertices[i] - vertices[j]).LengthSquared();
            }
            return sidesSquared.All(sqr => sqr < maxLengthSquared);
        }
    }

    public class LargeWidthFaceFilter : IFaceFilter
    {
        private const double DefaultMaxLength = 5;
        private double maxLengthSquared;
        private double _maxLength;
        public double MaxLength
        {
            get
            {
                return _maxLength;
            }
            set
            {
                _maxLength = value;
                maxLengthSquared = value * value;
            }
        }

        public LargeWidthFaceFilter(double maxLength = DefaultMaxLength)
        {
            MaxLength = maxLength;
        }

        public bool Check(Vector3[] vertices)
        {
            float[] sidesSquared = new float[3];
            //bool[] isVertical = new bool[3];

            // a, b - points on line
            // p - point
            // d - distance
            // s = (a - b)
            // p' = p - s
            // d = |(p' - a) x s| / |s|
            // d = |(p - s - a) x s|/|s|
            for (int i = 0; i < 3; i++)
            {
                var a = vertices[i];
                var b = vertices[(i + 1) % 3];
                var p = vertices[(i + 2) % 3];
                double angle1 = Math.Atan2(a.X, a.Z);
                double angle2 = Math.Atan2(b.X, b.Z);
                bool isVertical = Math.Abs(angle1 - angle2) < 1e-2;
                if (isVertical)
                {
                    Vector3 s = a - b;
                    double dSqr = Vector3.Cross((p - s - a), s).LengthSquared() / s.LengthSquared();
                    return dSqr <= maxLengthSquared;
                }
            }
            return true;
        }
    }
}
