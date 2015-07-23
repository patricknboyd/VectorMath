using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boyd.Math;

namespace VectorMath_Test
{
    [TestClass]
    public class Vector2_Test
    {
        public const double Epsilon = 0.0001;

        [TestMethod]
        public void Magnitude_Test()
        {
            Vector2 test = new Vector2(1.0, 0.0);

            Assert.AreEqual(1.0, test.Magnitude(), Epsilon);

            test = new Vector2(1.0, 1.0);
            Assert.AreEqual(1.414214, test.Magnitude(), Epsilon);

            test = new Vector2(-1.0, -1.0);
            Assert.AreEqual(1.414214, test.Magnitude(), Epsilon);
        }

        [TestMethod]
        public void Normalize_Test()
        {
            Vector2 test = new Vector2(1.0, 0.0);
            Vector2 normal = test.Normalize();

            Assert.AreEqual(1.0, normal.Magnitude());
            Assert.AreEqual(test, normal);

            test = new Vector2(1.0, 1.0);
            normal = test.Normalize();

            // Make sure the original vector is still the same.
            Assert.AreEqual(new Vector2(1.0, 1.0), test);
            Assert.AreEqual(1.0, normal.Magnitude(), Epsilon);
            Assert.AreEqual(new Vector2(0.707107, 0.707107), normal);
        }

        [TestMethod]
        public void IsNormal_Test_NonZero_ReturnsFalse()
        {
            Vector2 test = new Vector2(1, 2);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_Test_Zero_ReturnsFalse()
        {
            Vector2 test = new Vector2(0, 0);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_ReturnsTrue()
        {
            Vector2 test = new Vector2(1, 0);
            Assert.IsTrue(test.IsNormal());
        }

        [TestMethod]
        public void DotProduct_Orthogonal_ReturnsZero()
        {
            double result = Vector2.Right.Dot(Vector2.Up);

            Assert.AreEqual(0.0, result, Epsilon);
        }

        [TestMethod]
        public void DotProduct_SameVector_ReturnsMSquared()
        {
            Vector2 test = new Vector2(2.0, 0.0);

            double result = test.Dot(test);

            Assert.AreEqual(4.0, result, Epsilon);
        }

        [TestMethod]
        public void Addition_Test()
        {
            Vector2 result = new Vector2(1, 2) + new Vector2(4, 5);
            Assert.AreEqual(new Vector2(5, 7), result);

            result = new Vector2(1, 2) + new Vector2(-4, -5);
            Assert.AreEqual(new Vector2(-3, -3), result);

            result = new Vector2(1, 2) + Vector2.Zero;
            Assert.AreEqual(new Vector2(1, 2), result);

            Vector2 a = new Vector2(2, 6);
            result = Vector2.Zero;
            result += a;

            Assert.AreEqual(a, result);

        }
        [TestMethod]
        public void Subtraction_Test()
        {

            Vector2 result = new Vector2(1, 2) - new Vector2(4, 5);
            Assert.AreEqual(new Vector2(-3,-3), result);

            result = new Vector2(1, 2) - new Vector2(-4, -5);
            Assert.AreEqual(new Vector2(5,7), result);

            result = new Vector2(1, 2) - Vector2.Zero;
            Assert.AreEqual(new Vector2(1, 2), result);


            Vector2 a = new Vector2(2, 6);
            result = Vector2.Zero;
            result -= a;

            Assert.AreEqual(new Vector2(-2, -6), result);
        }

        [TestMethod]
        public void Multiplication_Test()
        {
            Vector2 result = new Vector2(1, 1) * 4;
            Assert.AreEqual(new Vector2(4, 4), result);

            result = new Vector2(1, -3) * -2;
            Assert.AreEqual(new Vector2(-2, 6), result);

            result = new Vector2(1, -3) * 0;
            Assert.AreEqual(Vector2.Zero, result);

            result = new Vector2(4, 3);
            result *= 3;
            Assert.AreEqual(new Vector2(12, 9), result);
        }

        [TestMethod]
        public void Division_Test()
        {
            Vector2 result = new Vector2(1, 1) / 4;
            Assert.AreEqual(new Vector2(0.25, 0.25), result);

            result = new Vector2(1, -3) / -2;
            Assert.AreEqual(new Vector2(-0.5, 1.5), result);

            result = Vector2.Zero / 1.5;
            Assert.AreEqual(Vector2.Zero, result);

            result = new Vector2(4, 3);
            result /= 3;
            Assert.AreEqual(new Vector2(1.33333, 1), result);
        }

        [TestMethod]
        public void Division_DivideByZeroTest()
        {
            Vector2 result = new Vector2(3, 3) / 0.0;
            Assert.IsTrue(double.IsInfinity(result.X));
            Assert.IsTrue(double.IsInfinity(result.Y));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Normalize_ZeroVector_Throws()
        {
            Vector2 result = Vector2.Zero.Normalize();
        }
    }
}
