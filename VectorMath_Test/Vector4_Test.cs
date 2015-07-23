using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Boyd.Math;

namespace VectorMath_Test
{
    [TestClass]
    public class Vector4_Test
    {

        public const double Epsilon = 0.0001;


        public Vector4 Right = new Vector4(1.0, 0.0, 0.0, 0.0);
        public Vector4 Up = new Vector4(0.0, 1.0, 0.0, 0.0);
        public Vector4 Forward = new Vector4(0.0, 0.0, 1.0, 0.0);

        public void ZeroConstructor_Success()
        {
            Vector4 vec = new Vector4();

            Assert.AreEqual(0.0, vec.X);
            Assert.AreEqual(0.0, vec.Y);
            Assert.AreEqual(0.0, vec.Z);
            Assert.AreEqual(0.0, vec.W);
        }

        public void ThreeInputConstructor_Success()
        {
            Vector4 vec = new Vector4(1.0, 2.0, 3.0);

            Assert.AreEqual(1.0, vec.X);
            Assert.AreEqual(2.0, vec.Y);
            Assert.AreEqual(3.0, vec.Z);
            Assert.AreEqual(1.0, vec.W);
        }

        public void FourInputConstructor_Success()
        {
            Vector4 vec = new Vector4(1.0, 2.0, 3.0, 4.0);

            Assert.AreEqual(1.0, vec.X);
            Assert.AreEqual(2.0, vec.Y);
            Assert.AreEqual(3.0, vec.Z);
            Assert.AreEqual(4.0, vec.W);
        }

        public void MagnitudeTest_Success()
        {
            Vector4 vec = new Vector4();

            Assert.AreEqual(0.0, vec.Magnitude(), Epsilon);

            vec = new Vector4(1.0, 0.0, 0.0);
            Assert.AreEqual(1.414, vec.Magnitude(), Epsilon);

            vec = new Vector4(1.0, 1.0, 1.0);
            Assert.AreEqual(2, vec.Magnitude(), Epsilon);

            vec = new Vector4(-1.0, -1.0, -1.0);
            Assert.AreEqual(2.0, vec.Magnitude(), Epsilon);

            vec = new Vector4(1.0, 0.0, 0.0, 0.0);
            Assert.AreEqual(1.0, vec.Magnitude(), Epsilon);

            vec = new Vector4(1.0, 1.0, 1.0, 1.0);
            Assert.AreEqual(2.0, vec.Magnitude(), Epsilon);

            vec = new Vector4(-1.0, -1.0, -1.0, -1.0);
            Assert.AreEqual(2.0, vec.Magnitude(), Epsilon);
        }

        [TestMethod]
        public void IsNormal_Test_NonZero_ReturnsFalse()
        {
            Vector4 test = new Vector4(1, 2, 3, 4);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_Test_Zero_ReturnsFalse()
        {
            Vector4 test = new Vector4(0, 0, 0, 0);

            Assert.IsFalse(test.IsNormal());
        }

        [TestMethod]
        public void IsNormal_ReturnsTrue()
        {
            Vector4 test = new Vector4(1, 0, 0, 0);
            Assert.IsTrue(test.IsNormal());
        }

        [TestMethod]
        public void Normalize_Test()
        {
            Vector4 test = new Vector4(1.0, 0.0, 0.0, 0.0);
            Vector4 normal = test.Normalize();

            Assert.AreEqual(1.0, normal.Magnitude());
            Assert.AreEqual(test, normal);

            test = new Vector4(1.0, 1.0, 1.0, 1.0);
            normal = test.Normalize();

            // Make sure the original vector is still the same.
            Assert.AreEqual(new Vector4(1.0, 1.0, 1.0, 1.0), test);
            Assert.AreEqual(1.0, normal.Magnitude(), Epsilon);
            Assert.AreEqual(new Vector4(0.5, 0.5, 0.5, 0.5), normal);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Normalize_ZeroVector_Throws()
        {
            Vector4 result = (new Vector4()).Normalize();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Normalize_NullInput_Throws()
        {
            Vector4 result = Vector4.Normalize(null);
        }

        [TestMethod]
        public void DotProduct_Orthogonal_ReturnsZero()
        {
            double result = Right.Dot(Up);

            Assert.AreEqual(0.0, result, Epsilon);
        }

        [TestMethod]
        public void DotProduct_SameVector_ReturnsMSquared()
        {
            Vector4 test = new Vector4(2.0, 0.0, 0.0, 0.0);

            double result = test.Dot(test);

            Assert.AreEqual(4.0, result, Epsilon);
        }

        [TestMethod]
        public void Addition_Test()
        {
            Vector4 result = new Vector4(1, 2, 3, 4) + new Vector4(4, 5, 6, 7);
            Assert.AreEqual(new Vector4(5, 7, 9, 11), result);

            result = new Vector4(1, 2, 3, 4) + new Vector4(-4, -5, -6, -7);
            Assert.AreEqual(new Vector4(-3, -3, -3, -3), result);

            result = new Vector4(1, 2, 3, 4) + Vector4.Zero;
            Assert.AreEqual(new Vector4(1, 2, 3, 4), result);

        }
        [TestMethod]
        public void Subtraction_Test()
        {
            Vector4 result = new Vector4(1, 2, 3, 4) - new Vector4(4, 5, 6, 7);
            Assert.AreEqual(new Vector4(-3, -3, -3, -3), result);

            result = new Vector4(1, 2, 3, 4) - new Vector4(-4, -5, -6, -7);
            Assert.AreEqual(new Vector4(5, 7, 9, 11), result);

            result = new Vector4(1, 2, 3, 4) - Vector4.Zero;
            Assert.AreEqual(new Vector4(1, 2, 3, 4), result);
        }

        [TestMethod]
        public void Multiplication_Test()
        {
            Vector4 result = new Vector4(1, 1, 1, 1) * 4;
            Assert.AreEqual(new Vector4(4, 4, 4, 4), result);

            result = new Vector4(1, -3, 5, -7) * -2;
            Assert.AreEqual(new Vector4(-2, 6, -10, 14), result);

            result = new Vector4(1, -3, 5, -7) * 0;
            Assert.AreEqual(Vector4.Zero, result);

            result = new Vector4(4, 3, 2, 1);
            result *= 3;
            Assert.AreEqual(new Vector4(12, 9, 6, 3), result);
        }

        [TestMethod]
        public void Division_Test()
        {
            Vector4 result = new Vector4(1, 1, 1, 1) / 4;
            Assert.AreEqual(new Vector4(0.25, 0.25, 0.25, 0.25), result);

            result = new Vector4(1, -3, 9, -27) / -2;
            Assert.AreEqual(new Vector4(-0.5, 1.5, -4.5, 13.5), result);

            result = Vector4.Zero / 1.5;
            Assert.AreEqual(Vector4.Zero, result);

            result = new Vector4(4, 3, 2, 1);
            result /= 3;
            Assert.AreEqual(new Vector4(1.33333, 1, 0.66667, 0.33333), result);
        }

        [TestMethod]
        public void Division_DivideByZeroTest()
        {
            Vector4 result = new Vector4(3, 3, 3, 3) / 0.0;
            Assert.IsTrue(double.IsInfinity(result.X));
            Assert.IsTrue(double.IsInfinity(result.Y));
            Assert.IsTrue(double.IsInfinity(result.Z));
            Assert.IsTrue(double.IsInfinity(result.W));
        }

        [TestMethod]
        public void ScaledVector_Success()
        {
            Vector4 vec = new Vector4(4, 6, 8, 2);

            Vector3 scaled = vec.ScaledVector();

            Assert.AreEqual(new Vector3(2, 3, 4), scaled);
        }
    }
}
