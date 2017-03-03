using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    public enum Axis { X, Y, Z }

    public class AxisScaler
    {
        public bool ScaleX { get; private set; } = true;
        public bool ScaleY { get; private set; } = true;
        public bool ScaleZ { get; private set; } = true;

        public float InputMinX { get; private set; }
        public float InputMaxX { get; private set; }
        public float InputMinY { get; private set; }
        public float InputMaxY { get; private set; }
        public float InputMinZ { get; private set; }
        public float InputMaxZ { get; private set; }

        public float OutputMinX { get; private set; }
        public float OutputMaxX { get; private set; }
        public float OutputMinY { get; private set; }
        public float OutputMaxY { get; private set; }
        public float OutputMinZ { get; private set; }
        public float OutputMaxZ { get; private set; }

        private static float Convert(float val, float min, float max, float outMin, float outMax, bool scale)
        {
            if (scale)
                return (val - min) / (max - min);
            else
                return val;
        }

        public void Convert(float x, float y, float z, out float outX, out float outY, out float outZ)
        {
            outX = Convert(x, InputMinX, InputMaxX, OutputMinX, OutputMaxX, ScaleX);
            outY = Convert(y, InputMinY, InputMaxY, OutputMinY, OutputMaxY, ScaleY);
            outZ = Convert(z, InputMinZ, InputMaxZ, OutputMinZ, OutputMaxZ, ScaleZ);
        }

        private AxisScaler() { }
        
    }

    public class AxisInverter
    {
        public bool InvertX { get; private set; } = false;
        public bool InvertY { get; private set; } = false;
        public bool InvertZ { get; private set; } = false;

        public float ZeroX { get; private set; } = 0;
        public float ZeroY { get; private set; } = 0;
        public float ZeroZ { get; private set; } = 0;

        private static float Convert(float val, float zero, bool invert)
        {
            if (invert)
                return zero - val;
            else
                return val;
        }
        
        public void Convert(float x, float y, float z, out float outX, out float outY, out float outZ)
        {
            outX = Convert(x, ZeroX, InvertX);
            outY = Convert(y, ZeroY, InvertY);
            outZ = Convert(z, ZeroZ, InvertZ);
        }

        public AxisInverter CreateX(float zero = 0) => Create(zero, null, null);
        public AxisInverter CreateY(float zero = 0) => Create(null, zero, null);
        public AxisInverter CreateZ(float zero = 0) => Create(null, null, zero);

        public AxisInverter Create(float? zeroX, float? zeroY = 0, float? zeroZ = 0)
        {
            return new AxisInverter()
            {
                InvertX = zeroX.HasValue,
                ZeroX = zeroX ?? 0,
                InvertY = zeroY.HasValue,
                ZeroY = zeroY ?? 0,
                InvertZ = zeroZ.HasValue,
                ZeroZ = zeroZ ?? 0
            };
        }
    }

    public class VectorConverter
    {
        public Axis SourceX { get; private set; } = Axis.X;
        public Axis SourceY { get; private set; } = Axis.Y;
        public Axis SourceZ { get; private set; } = Axis.Z;
        public bool InvertX { get; private set; } = false;
        public bool InvertY { get; private set; } = false;
        public bool InvertZ { get; private set; } = false;
        public bool IgnoreScaleX { get; private set; } = false;
        public bool IgnoreScaleY { get; private set; } = false;
        public bool IgnoreScaleZ { get; private set; } = false;
        public float InvertZeroX { get; private set; } = 0;
        public float InvertZeroY { get; private set; } = 0;
        public float InvertZeroZ { get; private set; } = 0;
        public float InputMinX { get; private set; }
        public float InputMaxX { get; private set; }
        public float InputMinY { get; private set; }
        public float InputMaxY { get; private set; }
        public float InputMinZ { get; private set; }
        public float InputMaxZ { get; private set; }
        public float OutputMinX { get; private set; }
        public float OutputMaxX { get; private set; }
        public float OutputMinY { get; private set; }
        public float OutputMaxY { get; private set; }
        public float OutputMinZ { get; private set; }
        public float OutputMaxZ { get; private set; }

        private VectorConverter() { }

        private static float ConvertFromIn(float val, float min, float max, bool invert, float zero, bool ignoreScale)
        {
            if (invert) val = zero - val;
            if (ignoreScale) return val;
            return (val - min) / (max - min);
        }
        private static float ConvertToOut(float val, float outMin, float outMax, bool ignore)
        {
            if (ignore) return val;
            return outMin + val * (outMax - outMin);
        }
        
        private static T GetAxis<T>(Axis axis, T x, T y, T z)
        {
            switch (axis)
            {
                case Axis.X: return x;
                case Axis.Y: return y;
                case Axis.Z: return z;
                default: return default(T);
            }
        }

        /// <summary>
        ///  Конвертирует координаты.
        ///  Последовательность действий:
        ///  <para>Преобразование в соответствии с InputMin/Max и Invert</para>
        ///  <para>Обмен осей в соответствии с Source</para>
        ///  <para>Преобразование в соответствии с OutputMin/Max</para>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="z"></param>
        /// <param name="outX"></param>
        /// <param name="outY"></param>
        /// <param name="outZ"></param>
        public void Convert(float x, float y, float z, out float outX, out float outY, out float outZ)
        {
            float xIn = ConvertFromIn(x, InputMinX, InputMaxX, InvertX, InvertZeroX, IgnoreScaleX);
            float yIn = ConvertFromIn(y, InputMinY, InputMaxY, InvertY, InvertZeroY, IgnoreScaleY);
            float zIn = ConvertFromIn(z, InputMinZ, InputMaxZ, InvertZ, InvertZeroZ, IgnoreScaleZ);
            float x2 = GetAxis(SourceX, xIn, yIn, zIn);
            float y2 = GetAxis(SourceY, xIn, yIn, zIn);
            float z2 = GetAxis(SourceZ, xIn, yIn, zIn);
            bool ignoreX = GetAxis(SourceX, IgnoreScaleX, IgnoreScaleY, IgnoreScaleZ);
            bool ignoreY = GetAxis(SourceY, IgnoreScaleX, IgnoreScaleY, IgnoreScaleZ);
            bool ignoreZ = GetAxis(SourceZ, IgnoreScaleX, IgnoreScaleY, IgnoreScaleZ);
            outX = ConvertToOut(x2, OutputMinX, OutputMaxX, ignoreX);
            outY = ConvertToOut(y2, OutputMinY, OutputMaxY, ignoreY);
            outZ = ConvertToOut(z2, OutputMinZ, OutputMaxZ, ignoreZ);
        }

        public static VectorConverter SwapAxis(Axis x, Axis y, Axis z)
        {
            return new VectorConverter()
            {
                IgnoreScaleX = true,
                IgnoreScaleY = true,
                IgnoreScaleZ = true,
                SourceX = x,
                SourceY = y,
                SourceZ = z
            };
        }

        public static VectorConverter Offset(float x, float y, float z)
        {
            return new VectorConverter()
            {
                IgnoreScaleX = true,
                IgnoreScaleY = true,
                IgnoreScaleZ = true,
            };
        }

        public static VectorConverter ScreenToNormal(int width, int height)
        {
            return new VectorConverter()
            {
                InvertY = true,
                InvertZeroY = height,
                InputMinX = 0,
                InputMaxX = width,
                OutputMinX = -1,
                OutputMaxX = 1,

                InputMinY = 0,
                InputMaxY = height,
                OutputMinY = -1,
                OutputMaxY = 1,

                IgnoreScaleZ = true
            };
        }
    }
}