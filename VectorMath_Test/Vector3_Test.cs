using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boyd.Math;

namespace VectorMath_Test
{
    [TestClass]
    public class Vector3_Test
    {
        public const double Epsilon = 0.0001;

        [TestMethod]
        public void Magnitude_Test()
        {
            Vector3 test = new Vector3(1.0, 0.0, 0.0);

            Assert.AreEqual(1.0, test.Magnitude(), Epsilon);

            test = new Vector3(1.0, 1.0, 1.0);
            Assert.AreEqual(1.732, test.Magnitude(), Epsilon);

            test = new Vector3(-1.0, -1.0, 1.0);
            Assert.AreEqual(1.732, test.Magnitude(), Epsilon);

            test = new Vector3();
            Assert.AreEqual(0.0, test.Magnitude(), Epsilon);
        }

        [TestMethod]
        public void Normalize_Test()
        {
            Vector3 test = new Vector3(1.0, 0.0, 0.0);
            Vector3 normal = test.Normalize();

            Assert.AreEqual(1.0, normal.Magnitude());
            Assert.AreEqual(test, normal);

            test = new Vector3(1.0, 1.0, 1.0);
            normal = test.Normalize();

            // Make sure the original vector is still the same.
            Assert.AreEqual(new Vector3(1.0, 1.0, 1.0), test);
            Assert.AreEqual(1.0, normal.Magnitude(), Epsilon);
            Assert.AreEqual(new Vector3(0.5774, 0.5774, 0.5774), normal);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Normalize_ZeroVector_Throws()
        {
            Vector3 result = Vector3.Zero.Normalize();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Normalize_NullInput_Throws()
        {
            Vector3 result = Vector3.Normalize(null);
        }


        [TestMethod]
        public void IsNormal_Test_NonZero_ReturnsFalse()
        {
            Vector3 test = new Vector3(1, 2, 3);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_Test_Zero_ReturnsFalse()
        {
            Vector3 test = new Vector3(0, 0, 0);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_ReturnsTrue()
        {
            Vector3 test = new Vector3(1, 0, 0);
            Assert.IsTrue(test.IsNormal());
        }

        [TestMethod]
        public void CrossProduct_ReturnsZeroVector()
        {
            Vector3 test = new Vector3(1, 2, 3);

            Vector3 result = test.Cross(test);

            Assert.AreEqual(Vector3.Zero, result);
        }

        [TestMethod]
        public void CrossProduct_ReturnsZVector()
        {
            Vector3 result = Vector3.Right.Cross(Vector3.Up);

            Assert.AreEqual(Vector3.Forward, result);
        }

        [TestMethod]
        public void DotProduct_Orthogonal_ReturnsZero()
        {
            double result = Vector3.Right.Dot(Vector3.Up);

            Assert.AreEqual(0.0, result, Epsilon);
        }

        [TestMethod]
        public void DotProduct_SameVector_ReturnsMSquared()
        {
            Vector3 test = new Vector3(2.0, 0.0, 0.0);

            double result = test.Dot(test);

            Assert.AreEqual(4.0, result, Epsilon);
        }

        [TestMethod]
        public void Addition_Test()
        {
            Vector3 result = new Vector3(1, 2, 3) + new Vector3(4, 5, 6);
            Assert.AreEqual(new Vector3(5, 7, 9), result);

            result = new Vector3(1, 2, 3) + new Vector3(-4, -5, -6);
            Assert.AreEqual(new Vector3(-3, -3, -3), result);

            result = new Vector3(1, 2, 3) + Vector3.Zero;
            Assert.AreEqual(new Vector3(1, 2, 3), result);

        }
        [TestMethod]
        public void Subtraction_Test()
        {

            Vector3 result = new Vector3(1, 2, 3) - new Vector3(4, 5, 6);
            Assert.AreEqual(new Vector3(-3,-3,-3), result);

            result = new Vector3(1, 2, 3) - new Vector3(-4, -5, -6);
            Assert.AreEqual(new Vector3(5,7,9), result);

            result = new Vector3(1, 2, 3) - Vector3.Zero;
            Assert.AreEqual(new Vector3(1, 2, 3), result);
        }

        [TestMethod]
        public void Multiplication_Test()
        {
            Vector3 result = new Vector3(1, 1, 1) * 4;
            Assert.AreEqual(new Vector3(4, 4, 4), result);

            result = new Vector3(1,-3, 5) * -2;
            Assert.AreEqual(new Vector3(-2, 6, -10), result);

            result = new Vector3(1, -3, 5) * 0;
            Assert.AreEqual(Vector3.Zero, result);

            result = new Vector3(4, 3, 2);
            result *= 3;
            Assert.AreEqual(new Vector3(12, 9, 6), result);
        }

        [TestMethod]
        public void Division_Test()
        {
            Vector3 result = new Vector3(1, 1, 1) / 4;
            Assert.AreEqual(new Vector3(0.25, 0.25, 0.25), result);

            result = new Vector3(1, -3, 9) / -2;
            Assert.AreEqual(new Vector3(-0.5, 1.5, -4.5), result);

            result = Vector3.Zero / 1.5;
            Assert.AreEqual(Vector3.Zero, result);

            result = new Vector3(4, 3, 2);
            result /= 3;
            Assert.AreEqual(new Vector3(1.33333, 1, 0.66667), result);
        }

        [TestMethod]
        public void Division_DivideByZeroTest()
        {
            Vector3 result = new Vector3(3, 3, 3) / 0.0;
            Assert.IsTrue(double.IsInfinity(result.X));
            Assert.IsTrue(double.IsInfinity(result.Y));
            Assert.IsTrue(double.IsInfinity(result.Z));
        }
    }
}
