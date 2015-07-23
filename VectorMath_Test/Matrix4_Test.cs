using Boyd.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VectorMath_Test
{
    [TestClass]
    public class Matrix4_Test
    {
        private const double Epsilon = 0.0001;

        #region Constructor Tests

        [TestMethod]
        public void ZeroConstructorTest_Success()
        {
            Matrix4 mat = new Matrix4();

            for (int x = 0; x < 4; x++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Assert.AreEqual(0.0, mat[x, y]);
                }
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DoubleArrayConstructor_TooMany_Throws()
        {
            double[] values = new double[] {
                1.0,2.0,3.0,4.0,
                5.0,6.0,7.0,8.0,
                9.0,10.0,11.0,12.0,
                13.0,14.0,15.0,16.0,
                17.0
            };

            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DoubleArrayConstructor_Null_Throws()
        {
            double[] values = null;
            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        public void DoubleArrayConstructor_TooFew_Success()
        {
            double[] values = new double[] {
                1.0,2.0,3.0,4.0,
                5.0,6.0,7.0,8.0,
                9.0,10.0,11.0,12.0
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(0.0, mat[3, 0]);
        }

        [TestMethod]
        public void DoubleArrayConstructor_Success()
        {
            double[] values = new double[] {
                1.0,2.0,3.0,4.0,
                5.0,6.0,7.0,8.0,
                9.0,10.0,11.0,12.0,
                13.0,14.0,15.0,16.0,
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(14.0, mat[3, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiDoubleArrayConstructor_TooManyRows_Throws()
        {
            double[,] values = new double[,] {
                { 1.0,2.0,3.0,4.0 },
                {5.0,6.0,7.0,8.0},
                {9.0,10.0,11.0,12.0},
                {13.0,14.0,15.0,16.0},
                {17.0, 18.0, 19.0, 20.0 }
            };

            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MultiDoubleArrayConstructor_TooManyColumns_Throws()
        {
            double[,] values = new double[,] {
                { 1.0,2.0,3.0,4.0, 1.0 },
                {5.0,6.0,7.0,8.0, 1.0},
                {9.0,10.0,11.0,12.0, 1.0},
                {13.0,14.0,15.0,16.0, 1.0}
            };

            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MultiDoubleArrayConstructor_Null_Throws()
        {
            double[,] values = null;
            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        public void MultiDoubleArrayConstructor_TooFew_Success()
        {
            double[,] values = new double[,] {
                { 1.0,2.0,3.0,4.0 },
                {5.0,6.0,7.0,8.0},
                {9.0,10.0,11.0,12.0}
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(0.0, mat[3, 0]);
        }

        [TestMethod]
        public void MultiDoubleArrayConstructor_Success()
        {
            double[,] values = new double[,] {
                { 1.0,2.0,3.0,4.0 },
                {5.0,6.0,7.0,8.0},
                {9.0,10.0,11.0,12.0},
                {13.0,14.0,15.0,16.0}
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(14.0, mat[3, 1]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void VectorArrayConstructor_TooMany_Throws()
        {
            Vector4[] values = new Vector4[] {
                new Vector4(1.0, 2.0, 3.0, 4.0),
                new Vector4(5.0, 6.0, 7.0, 8.0),
                new Vector4(9.0, 10.0, 11.0, 12.0),
                new Vector4(13.0, 14.0, 15.0, 16.0),
                new Vector4(17.0, 18.0, 18.0, 20.0),
            };

            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void VectorArrayConstructor_NullInArray_Throws()
        {
            Vector4[] values = new Vector4[] {
                new Vector4(1.0, 2.0, 3.0, 4.0),
                new Vector4(5.0, 6.0, 7.0, 8.0),
                null,
                new Vector4(13.0, 14.0, 15.0, 16.0),
            };

            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VectorArrayConstructor_Null_Throws()
        {
            Vector4[] values = null;
            Matrix4 mat = new Matrix4(values);
        }

        [TestMethod]
        public void VectorArrayConstructor_TooFew_Success()
        {
            Vector4[] values = new Vector4[] {
                new Vector4(1.0, 2.0, 3.0, 4.0),
                new Vector4(5.0, 6.0, 7.0, 8.0),
                new Vector4(9.0, 10.0, 11.0, 12.0),
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(0.0, mat[3, 0]);
        }

        [TestMethod]
        public void VectorArrayConstructor_Success()
        {
            Vector4[] values = new Vector4[] {
                new Vector4(1.0, 2.0, 3.0, 4.0),
                new Vector4(5.0, 6.0, 7.0, 8.0),
                new Vector4(9.0, 10.0, 11.0, 12.0),
                new Vector4(13.0, 14.0, 15.0, 16.0),
            };

            Matrix4 mat = new Matrix4(values);

            Assert.AreEqual(1.0, mat[0, 0]);
            Assert.AreEqual(7.0, mat[1, 2]);
            Assert.AreEqual(14.0, mat[3, 1]);
        }

        #endregion

        #region Transpose Tests

        [TestMethod]
        public void Transpose_Success()
        {
            Matrix4 mat = new Matrix4(new double[,]
            {
                { 1.0, 2.0, 3.0, 4.0 },
                { 5.0, 6.0, 7.0, 8.0 },
                { 9.0, 10.0, 11.0, 12.0 },
                { 13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 transpose = mat.Transpose();

            Assert.AreEqual(1.0, transpose[0, 0], Epsilon);
            Assert.AreEqual(5.0, transpose[0, 1], Epsilon);
            Assert.AreEqual(7.0, transpose[2, 1], Epsilon);
            Assert.AreEqual(9.0, transpose[0, 2], Epsilon);
        }

        [TestMethod]
        public void Transpose_Empty_Success()
        {
            Matrix4 mat = new Matrix4();
            Matrix4 transpose = mat.Transpose();

            Assert.AreEqual(0.0, transpose[0, 0], Epsilon);
            Assert.AreEqual(0.0, transpose[0, 1], Epsilon);
            Assert.AreEqual(0.0, transpose[2, 1], Epsilon);
            Assert.AreEqual(0.0, transpose[0, 2], Epsilon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Tranpose_Null_Throws()
        {
            Matrix4 mat = null;

            Matrix4 transpose = Matrix4.Transpose(mat);
        }

        #endregion

        #region Scalar Operations

        [TestMethod]
        public void AdditionTest_Success()
        {

            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 b = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 result = a + b;


            Matrix4 expected = new Matrix4(new double[,]
            {
                {2.0, 4.0, 6.0, 8.0},
                {10.0, 12.0, 14.0, 16.0},
                {18.0, 20.0, 22.0, 24.0},
                {26.0, 28.0, 30.0, 32.0}
            });

            Assert.AreEqual(expected, result);


        }

        [TestMethod]
        public void SubtractionTest_Success()
        {

            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 b = new Matrix4(new double[,]
            {
                {4.0, 3.0, 2.0, 1.0},
                {8.0, 7.0, 6.0, 5.0},
                {12.0, 11.0, 10.0, 9.0},
                {16.0, 15.0, 14.0, 13.0}
            });

            Matrix4 result = a - b;


            Matrix4 expected = new Matrix4(new double[,]
            {
                {-3.0, -1.0, 1.0, 3.0},
                {-3.0, -1.0, 1.0, 3.0},
                {-3.0, -1.0, 1.0, 3.0},
                {-3.0, -1.0, 1.0, 3.0},
            });

            Assert.AreEqual(expected, result);


        }

        [TestMethod]
        public void MultiplicationTest()
        {
            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 result = a * 2;


            Matrix4 expected = new Matrix4(new double[,]
            {
                { 2.0, 4.0, 6.0, 8.0 },
                { 10.0, 12.0, 14.0, 16.0 },
                { 18.0, 20.0, 22.0, 24.0 },
                { 26.0, 28.0, 30.0, 32.0 }
            });

            Assert.AreEqual(expected, result);

            result = a * 0;

            expected = new Matrix4();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void DivisionTest()
        {
            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 result = a / 2;


            Matrix4 expected = new Matrix4(new double[,]
            {
                { 0.5, 1.0, 1.5, 2.0 },
                { 2.5, 3.0, 3.5, 4.0 },
                { 4.5, 5.0, 5.5, 6.0 },
                { 6.5, 7.0, 7.5, 8.0 }
            });

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivisionTest_DivideByZero_Throws()
        {
            Matrix4 a = new Matrix4(new double[,]
            {
                
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 result = a / 0;
        }

        #endregion

        #region Matrix Multiplication Tests

        [TestMethod]
        public void MultiplyByIdentity_ReturnsSame()
        {
            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 result = a * Matrix4.Identity;

            Assert.AreEqual(a, result);
        }

        [TestMethod]
        public void MultiplyTest_MatrixByMatrix()
        {
            Matrix4 a = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Matrix4 b = new Matrix4(new double[,]
            {
                {4.0, 3.0, 2.0, 1.0},
                {8.0, 7.0, 6.0, 5.0},
                {12.0, 11.0, 10.0, 9.0},
                {16.0, 15.0, 14.0, 13.0}
            });

            var result = a * b;

            Matrix4 expected = new Matrix4(new double[,]
            {
                { 120.0, 110.0, 100.0, 90.0 },
                { 280.0, 254.0, 228.0, 202.0 },
                { 440.0, 398.0, 356.0, 314.0 },
                { 600.0, 542.0, 484.0, 426.0 }
            });

            Assert.AreEqual(expected, result);

            result = b * a;

            expected = new Matrix4(new double[,] {
                { 50.0, 60.0, 70.0, 80.0 },
                { 162.0, 188.0, 214.0, 240.0 },
                { 274.0, 316.0, 358.0, 400.0 },
                { 386.0, 444.0, 502.0, 560.0 }
            });

            Assert.AreEqual(expected, result);

        }
        [TestMethod]
        public void MultiplyTest_VectorByMatrix()
        {
            Vector4 a = new Vector4(1.0, 2.0, 3.0, 4.0);
            Matrix4 b = new Matrix4(new double[,]
            {
                {1.0, 2.0, 3.0, 4.0},
                {5.0, 6.0, 7.0, 8.0},
                {9.0, 10.0, 11.0, 12.0},
                {13.0, 14.0, 15.0, 16.0}
            });

            Vector4 result = a * b;

            Vector4 expected = new Vector4(90.0, 100.0, 110.0, 120.0);

            Assert.AreEqual(expected, result);

            result = b * a;

            expected = new Vector4(30.0, 70.0, 110.0, 150.0);

            Assert.AreEqual(expected, result);
        }

        #endregion


        [TestMethod]
        public void DeterminantTest_Success()
        {
            Matrix4 mat = new Matrix4(new double[,]
            {
                { -2.0, 2.0, -3.0, 3.0 },
                { -1.0, 1.0, 3.0, -3.0 },
                { 2.0, 0.0, -1.0, 1.0 },
                { -3.0, 2.0, -1.0, 0.0 }
            });

            double expected = -18.0;
            double actual = mat.Determinant();

            Assert.AreEqual(expected, actual, 0.001);
        }

        [TestMethod]
        public void InversionTest_Success()
        {
            Matrix4 mat = new Matrix4(new double[,]
            {
                { -2.0, 2.0, -3.0, -3.0 },
                { -1.0, -1.0, 3.0, 3.0 },
                { 2.0, 0.0, -1.0, 1.0 },
                { 3.0, 2.0, 1.0, 0.0 }
            });

            Matrix4 inverse = mat.Inverse();

            Matrix4 product = mat * inverse;

            Assert.AreEqual(Matrix4.Identity, product);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InversionTest_NonInvertible_Throws()
        {
            Matrix4 mat = new Matrix4(new double[,]
            {
                
                { -2.0, 2.0, -3.0, 3.0 },
                { -1.0, 1.0, 3.0, -3.0 },
                { 2.0, 0.0, -1.0, 1.0 },
                { -3.0, 2.0, -1.0, 0.0 }
            });

            mat.Inverse();
        }
    }
}
