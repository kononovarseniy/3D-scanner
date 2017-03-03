using Microsoft.VisualStudio.TestTools.UnitTesting;
using Scan3D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Scan3D.Tests
{
    [TestClass()]
    public class CylinderTests
    {
        [TestMethod()]
        public void ContainsTest()
        {
            Cylinder c = new Cylinder(-10, 20, 5);
            Assert.IsTrue(c.Contains(new Vector3(4, -10, 3)));
            Assert.IsTrue(c.Contains(new Vector3(3, 20, 4)));
            Assert.IsTrue(c.Contains(new Vector3(4, 0, 2)));
            Assert.IsTrue(c.Contains(new Vector3(-3, 10, 3)));
            Assert.IsTrue(c.Contains(new Vector3(4, 10, -2)));
            Assert.IsTrue(c.Contains(new Vector3(0, 10, -3)));

            Assert.IsFalse(c.Contains(new Vector3(4, -20, 3)));
            Assert.IsFalse(c.Contains(new Vector3(5, 20, 3)));
            Assert.IsFalse(c.Contains(new Vector3(5, 0, 5)));
            Assert.IsFalse(c.Contains(new Vector3(5, 100, 5)));
        }
    }
}