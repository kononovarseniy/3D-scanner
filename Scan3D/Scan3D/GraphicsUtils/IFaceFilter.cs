using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D.GraphicsUtils
{
    public interface IFaceFilter
    {
        bool Check(Vector3[] vertices);
    }
}
