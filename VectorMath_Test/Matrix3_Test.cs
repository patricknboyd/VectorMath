using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Boyd.Math;

namespace VectorMath_Test
{
    [TestClass]
    public class Matrix3_Test
    {
        private const double Epsilon = 0.0001;

        #region Constructor Tests

        #region FromFlatArray Tests

        [TestMethod]
        public void Constructor_FromFlatArray_Success()
        {
            double[] input = new double[] {
                1.0, 2.0, 3.0,
                4.0, 5.0, 6.0,
                7.0, 8.0, 9.0 };

            Matrix3 mat = new Matrix3(input);

            Assert.AreEqual(1.0, mat[0, 0], Epsilon);
            Assert.AreEqual(4.0, mat[1, 0], Epsilon);
            Assert.AreEqual(9.0, mat[2, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_FromFlatArray_TooBig_Throws()
        {
            double[] input = new double[] {
                1.0, 2.0, 3.0,
                4.0, 5.0, 6.0,
                7.0, 8.0, 9.0, 10.0 };

            Matrix3 mat = new Matrix3(input);
        }
        [TestMethod]
        public void Constructor_FromFlatArray_TooSmall_Success()
        {
            double[] input = new double[] {
                1.0, 2.0, 3.0,
                4.0, 5.0, 6.0 };

            Matrix3 mat = new Matrix3(input);

            Assert.AreEqual(1.0, mat[0, 0], Epsilon);
            Assert.AreEqual(4.0, mat[1, 0], Epsilon);
            Assert.AreEqual(6.0, mat[1, 2], Epsilon);
            Assert.AreEqual(0.0, mat[2, 2], Epsilon);
        }
        [TestMethod]
        public void Constructor_FromFlatArray_Empty_Success()
        {
            double[] empty = new double[0];

            Matrix3 mat = new Matrix3(empty);

            Assert.AreEqual(0.0, mat[0, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 2], Epsilon);
            Assert.AreEqual(0.0, mat[2, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FromFlatArray_NullParameter_Throws()
        {
            double[] input = null;

            Matrix3 mat = new Matrix3(input);
        }

        #endregion

        #region From Two Dimensional Array Tests

        [TestMethod]
        public void Constructor_FromTwoDArray_Success()
        {
            double[,] input = new double[,] {
                { 1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0} };

            Matrix3 mat = new Matrix3(input);

            Assert.AreEqual(1.0, mat[0, 0], Epsilon);
            Assert.AreEqual(4.0, mat[1, 0], Epsilon);
            Assert.AreEqual(9.0, mat[2, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_FromTwoDArray_TooBig_Throws()
        {
            double[,] input = new double[,] {
                { 1.0, 2.0, 3.0, 0.0 },
                {4.0, 5.0, 6.0, 0.0},
                {7.0, 8.0, 9.0, 10.0} };

            Matrix3 mat = new Matrix3(input);
        }
        [TestMethod]
        public void Constructor_FromTwoDArray_TooSmall_Success()
        {
            double[,] input = new double[,] {
                { 1.0, 2.0, 3.0 },
                {4.0, 5.0, 6.0} 
            };

            Matrix3 mat = new Matrix3(input);

            Assert.AreEqual(1.0, mat[0, 0], Epsilon);
            Assert.AreEqual(4.0, mat[1, 0], Epsilon);
            Assert.AreEqual(6.0, mat[1, 2], Epsilon);
            Assert.AreEqual(0.0, mat[2, 2], Epsilon);
        }
        [TestMethod]
        public void Constructor_FromTwoDArray_Empty_Success()
        {
            double[,] empty = new double[0,0];

            Matrix3 mat = new Matrix3(empty);

            Assert.AreEqual(0.0, mat[0, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 2], Epsilon);
            Assert.AreEqual(0.0, mat[2, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_FromTwoDArray_NullParameter_Throws()
        {
            double[,] input = null;

            Matrix3 mat = new Matrix3(input);
        }
        #endregion

        [TestMethod]
        public void EmptyConstructor_Success()
        {
            Matrix3 mat = new Matrix3();

            Assert.AreEqual(0.0, mat[0, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 0], Epsilon);
            Assert.AreEqual(0.0, mat[1, 2], Epsilon);
            Assert.AreEqual(0.0, mat[2, 2], Epsilon);
        }

        #endregion

        #region Transpose Tests

        [TestMethod]
        public void Transpose_Success()
        {
            Matrix3 mat = new Matrix3(new double[,]
            {
                { 1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 transpose = mat.Transpose();

            Assert.AreEqual(1.0, transpose[0, 0], Epsilon);
            Assert.AreEqual(4.0, transpose[0,1], Epsilon);
            Assert.AreEqual(6.0, transpose[2,1], Epsilon);
            Assert.AreEqual(7.0, transpose[0,2], Epsilon);
        }

        [TestMethod]
        public void Transpose_Empty_Success()
        {
            Matrix3 mat = new Matrix3();
            Matrix3 transpose = mat.Transpose();

            Assert.AreEqual(0.0, transpose[0, 0], Epsilon);
            Assert.AreEqual(0.0, transpose[0, 1], Epsilon);
            Assert.AreEqual(0.0, transpose[2, 1], Epsilon);
            Assert.AreEqual(0.0, transpose[0, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tranpose_Null_Throws()
        {
            Matrix3 mat = null;

            Matrix3 transpose = Matrix3.Transpose(mat);
        }

        #endregion

        #region Scalar Operators Test

        [TestMethod]
        public void AdditionTest_Success()
        {

            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 b = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 result = a + b;


            Matrix3 expected = new Matrix3(new double[,]
            {
                {2.0, 4.0, 6.0},
                {8.0, 10.0, 12.0},
                {14.0, 16.0, 18.0}
            });

            Assert.AreEqual(expected, result);


        }

        [TestMethod]
        public void SubtractionTest_Success()
        {

            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 b = new Matrix3(new double[,]
            {
                {3.0, 2.0, 1.0},
                {6.0, 5.0, 4.0},
                {9.0, 8.0, 7.0}
            });

            Matrix3 result = a - b;


            Matrix3 expected = new Matrix3(new double[,]
            {
                {-2.0, 0.0, 2.0},
                {-2.0, 0.0, 2.0},
                {-2.0, 0.0, 2.0}
            });

            Assert.AreEqual(expected, result);


        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 result = a * 2;


            Matrix3 expected = new Matrix3(new double[,]
            {
                { 2.0, 4.0, 6.0},
                { 8.0, 10.0, 12.0},
                {14.0, 16.0, 18.0}
            });

            Assert.AreEqual(expected, result);

            result = a * 0;

            expected = new Matrix3();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DivisionTest()
        {
            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 result = a / 2;


            Matrix3 expected = new Matrix3(new double[,]
            {
                { 0.5, 1.0, 1.5},
                { 2.0, 2.5, 3.0},
                { 3.5, 4.0, 4.5}
            });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionTest_DivideByZero_Throws()
        {
            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 result = a / 0;
        }

        #endregion

        #region Matrix Multiplication Tests

        [TestMethod]
        public void MultiplyByIdentity_ReturnsSame()
        {
            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 result = a * Matrix3.Identity;

            Assert.AreEqual(a, result);
        }

        [TestMethod]
        public void MultiplyTest_MatrixByMatrix()
        {
            Matrix3 a = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Matrix3 b = new Matrix3(new double[,]
            {
                {3.0, 2.0, 1.0},
                {6.0, 5.0, 4.0},
                {9.0, 8.0, 7.0}
            });

            var result = a * b;

            Matrix3 expected = new Matrix3(new double[,]
            {
                {42.0, 36.0, 30.0},
                {96.0, 81.0, 66.0},
                {150.0, 126.0, 102.0}
            });

            Assert.AreEqual(expected, result);

            result = b * a;

            expected = new Matrix3(new double[,] {
                { 18.0, 24.0, 30.0 },
                { 54.0, 69.0, 84.0 },
                { 90.0, 114.0, 138.0 }
            });

            Assert.AreEqual(expected, result);

        }
        [TestMethod]
        public void MultiplyTest_VectorByMatrix()
        {
            Vector3 a = new Vector3(1.0, 2.0, 3.0);
            Matrix3 b = new Matrix3(new double[,]
            {
                {1.0, 2.0, 3.0},
                {4.0, 5.0, 6.0},
                {7.0, 8.0, 9.0}
            });

            Vector3 result = a * b;

            Vector3 expected = new Vector3(30.0, 36.0, 42.0);

            Assert.AreEqual(expected, result);

            result = b * a;

            expected = new Vector3(14.0, 32.0, 50.0);

            Assert.AreEqual(expected, result);
        }


        #endregion


        [TestMethod]
        public void DeterminantTest_Success()
        {
            Matrix3 mat = new Matrix3(new double[,]
            {
                { -2.0, 2.0, -3.0 },
                { -1.0, 1.0, 3.0 },
                { 2.0, 0.0, -1.0 }
            });

            double expected = 18.0;
            double actual = mat.Determinant();

            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void InversionTest_Success()
        {
            Matrix3 mat = new Matrix3(new double[,]
            {
                { -2.0, 2.0, -3.0 },
                { -1.0, 1.0, 3.0 },
                { 2.0, 0.0, -1.0 }
            });

            Matrix3 inverse = mat.Inverse();

            Matrix3 product = mat * inverse;

            Assert.AreEqual(Matrix3.Identity, product);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InversionTest_NonInvertible_Throws()
        {
            Matrix3 mat = new Matrix3(new double[,]
            {
                { -2.0, 2.0, -3.0 },
                { 0.0, 0.0, 0.0 },
                { 2.0, 0.0, -1.0 }
            });

            mat.Inverse();
        }
    }
}
