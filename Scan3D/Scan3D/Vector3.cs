using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D
{
    public class MyVector3
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double Z { get; private set; }

        public MyVector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }
}
