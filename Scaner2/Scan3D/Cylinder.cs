using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    public class Cylinder
    {
        private double RadiusSqr;

        /// <summary>
        /// Y coordinate of bottom side of cylinder.
        /// </summary>
        public float Bottom { get; private set; }
        /// <summary>
        /// Y coordinate of top side of cylinder.
        /// </summary>
        public float Top { get; private set; }
        /// <summary>
        /// Radius of cylinder.
        /// </summary>
        public float Radius => (float)Math.Sqrt(RadiusSqr);

        public Cylinder(float bottom, float top, float radius)
        {
            Bottom = bottom;
            Top = top;
            RadiusSqr = radius * radius;
        }

        /// <summary>
        /// Determines if the specified point is contained in this cylinder.
        /// </summary>
        /// <returns>True if point cilinder contains point, otherwise false.</returns>
        public bool Contains(Vector3 point)
        {
            return point.Y >= Bottom &&
                point.Y <= Top &&
                (point.X * point.X + point.Z * point.Z) <= RadiusSqr;
        }
    }
}
