using System;
using System.Numerics;

namespace Scan3D
{
    //     Y ^
    //       |   / Z
    //       |  /
    //       | /
    //       |/      X
    // ------O------->
    //      /|
    //     / |
    //    /  |
    //   /   |
    /// <summary>
    /// Transforms cordinates on the image to real world coordinates
    /// </summary>
    class PointScanner
    {
        /// <summary>
        /// Horizontal field of view in radians
        /// </summary>
        public double FovX { get; set; }
        /// <summary>
        /// Image ratio. Width/Height
        /// </summary>
        public double Ratio { get; set; }
        /// <summary>
        /// Position of camera, in millimeters, rellative to platform center.
        /// </summary>
        public Vector3 CameraPosition { get; set; }
        /// <summary>
        /// Camera pitch angle, in radians, around the X axis. 
        /// </summary>
        public double CameraPitch { get; set; }
        /// <summary>
        /// Camera yaw angle, in radians, around the Y axis. 
        /// </summary>
        public double CameraYaw { get; set; }
        /// <summary>
        /// Angle, in radians, between laser plane and Z axis.
        /// </summary>
        public double LaserAngle { get; set; }

        private static double CoordToAngle(double coord, double fov)
        {
            double alf = fov / 2;
            double a = 1 / Math.Tan(alf);
            double bet = Math.Atan2(coord, a);
            return bet;
        }

        private static double AngleToCoord(double angle, double fov)
        {
            double alf = fov / 2;
            double a = 1 / Math.Tan(alf);
            return a * Math.Tan(angle);
        }

        private Vector3 GetDirection(double x, double y)
        {
            double yaw = CoordToAngle(x, FovX);
            double pitch = CoordToAngle(y, FovX / Ratio);
            return new Vector3(
                x: (float)Math.Tan(yaw + CameraYaw),
                y: (float)Math.Tan(pitch + CameraPitch),
                z: 1);
        }

        private static Vector3 GetLaserVector(double angle)
        {
            return new Vector3((float)Math.Sin(angle), 0, (float)Math.Cos(angle));
        }

        private Vector3 GetIntersection(Vector3 a, Vector3 b, Vector3 v, Vector3 c)
        {
            // a*A + b*B = c + v*C

            double tmp_1 = a.Y * b.Z - a.Z * b.Y;
            double tmp_2 = a.Z * b.X - a.X * b.Z;
            double tmp_3 = a.X * b.Y - a.Y * b.X;
            double CNum = -(tmp_1 * c.X + tmp_2 * c.Y + tmp_3 * c.Z);
            double CDenom = tmp_1 * v.X + tmp_2 * v.Y + tmp_3 * v.Z;

            return c + Vector3.Multiply(v, (float)(CNum / CDenom));
        }

        public double XToInternal(double x, int w) => x * 2d / w - 1;
        public double YToInternal(double y, int h) => 1 - y * 2d / h;
        public double XFromInternal(double x, int w) => (x + 1) * w / 2;
        public double YFromInternal(double y, int h) => (1 - y) * h / 2;

        private Vector3 ConvertTo3D(double x, double y)
        {
            var laser = GetLaserVector(LaserAngle);
            var direction = GetDirection(x, y);
            return GetIntersection(Vector3.UnitY, laser, direction, CameraPosition);
        }

        private Vector2 ConvertTo2D(Vector3 p)
        {
            Vector3 rel = p - CameraPosition;
            double yaw = Math.Atan2(rel.X, rel.Z) - CameraYaw;
            double pitch = Math.Atan2(rel.Y, rel.Z) - CameraPitch;
            return new Vector2(
                x: (float)AngleToCoord(yaw, FovX),
                y: (float)AngleToCoord(pitch, FovX / Ratio));
        }

        private void Calibrate(double zeroX, double zeroY)
        {
            double pitch = CoordToAngle(zeroY, FovX / Ratio);
            double yaw = CoordToAngle(zeroX, FovX);
            
            double perfectPitch = Math.Atan2(-CameraPosition.Y, -CameraPosition.Z);

            CameraPitch = perfectPitch - pitch;
            CameraYaw = -yaw;
        }

        public Vector3 ConvertTo3D(double x, double y, int width, int height)
        {
            return ConvertTo3D(XToInternal(x, width), YToInternal(y, height));
        }
        public Vector3 ConvertTo3D(Vector2 p, int width, int height)
        {
            return ConvertTo3D(p.X, p.Y, width, height);
        }
        
        public Vector2 ConvertTo2D(Vector3 p, int width, int height)
        {
            var np = ConvertTo2D(p);
            return new Vector2(
                (float)XFromInternal(np.X, width),
                (float)YFromInternal(np.Y, height));
        }

        public void Calibrate(double zeroX, double zeroY, int width, int height)
        {
            Calibrate(XToInternal(zeroX, width), YToInternal(zeroY, height));
        }
        public void Calibrate(Vector2 zero, int width, int height)
        {
            Calibrate(zero.X, zero.Y, width, height);
        }
    }
}