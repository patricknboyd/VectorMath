using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Math
{
    /// <summary>
    /// Represents a 3x3 matrix.
    /// </summary>
    public class Matrix3
    {
        private double[,] _values;
        private const int Height = 3;
        private const int Width = 3;

        /// <summary>
        /// Creates a new zero matrix.
        /// </summary>
        public Matrix3()
        {
            _values = new double[,]
            {
                {0.0, 0.0, 0.0},
                {0.0, 0.0, 0.0},
                {0.0, 0.0, 0.0}
            };
        }

        /// <summary>
        /// Creates a new matrix with the values given in the array.
        /// </summary>
        /// <param name="values"></param>
        /// <remarks>Values will be interpreted as a series of rows, any missing values will be replaced with 0.</remarks>
        public Matrix3(double[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length > 9)
            {
                throw new ArgumentException(ErrorStrings.TooManyValuesForMatrix);
            }

            _values = new double[Height, Width];

            int index = 0;

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    _values[y, x] = index >= values.Length ? 0.0 : values[index];
                    index++;
                }
            }
        }

        /// <summary>
        /// Creates a new matrix with the given values.
        /// </summary>
        /// <param name="values">The indices for the array are [column,row]. Any missing values will be filled in with 0.</param>
        public Matrix3(double[,] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }

            int inputHeight = values.GetLength(0);
            int inputWidth = values.GetLength(1);

            if (inputHeight > Height || inputWidth > Width)
            {
                throw new ArgumentException(ErrorStrings.TooManyValuesForMatrix);
            }

            _values = new double[Height, Width];

            for (int y = 0; y < Height; y++)
            {
                if (y >= inputHeight)
                {
                    _values[y, 0] = 0.0;
                    _values[y, 1] = 0.0;
                    _values[y, 2] = 0.0;
                }
                else
                {
                    for (int x = 0; x < Width; x++)
                    {
                        if (x >= inputWidth)
                        {
                            _values[y, x] = 0.0;
                        }
                        else
                        {
                            _values[y, x] = values[y, x];
                        }
                    }
                }
            }
        }

        public Matrix3(Vector3[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length > 3)
            {
                throw new ArgumentException(ErrorStrings.TooManyValuesForMatrix);
            }

            _values = new double[Height, Width];

            for (int y = 0; y < values.Length; y++)
            {
                _values[y, 0] = values[y].X;
                _values[y, 1] = values[y].Y;
                _values[y, 2] = values[y].Z;
            }

            for (int i = values.Length; i < Height; i++)
            {
                _values[i, 0] = 0.0;
                _values[i, 1] = 0.0;
                _values[i, 2] = 0.0;
            }
        }

        public Matrix3 Transpose()
        {
            return Matrix3.Transpose(this);
        }

        public double Determinant()
        {
            double determ =
                (_values[0, 0] * _values[1, 1] * _values[2, 2]) +
                (_values[0, 1] * _values[1, 2] * _values[2, 0]) +
                (_values[0, 2] * _values[1, 0] * _values[2, 1]);

            determ -=
                (_values[0, 2] * _values[1, 1] * _values[2, 0]) +
                (_values[0, 0] * _values[1, 2] * _values[2, 1]) +
                (_values[0, 1] * _values[1, 0] * _values[2, 2]);

            return determ;
        }

        public Matrix3 Inverse()
        {
            double determ = this.Determinant();

            if (determ <= 0.0)
            {
                throw new InvalidOperationException("This matrix is not invertible.");
            }

            Vector3 first = new Vector3(_values[0, 0], _values[1, 0], _values[2, 0]);
            Vector3 second = new Vector3(_values[0, 1], _values[1, 1], _values[2, 1]);
            Vector3 third = new Vector3(_values[0, 2], _values[1, 2], _values[2, 2]);

            Matrix3 inverse = new Matrix3(
                new Vector3[] {
                    Vector3.Cross(second, third),
                    Vector3.Cross(third, first),
                    Vector3.Cross(first, second)
                });

            return inverse / determ;
        }

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            for (int y = 0; y < 3; y++)
            {
                output.AppendFormat("[{0:0.###}, {1:0.###}, {2:0.###}]", _values[y,0], _values[y,1], _values[y,2]);
                output.AppendLine();
            }

            return output.ToString();
        }

        public override bool Equals(object obj)
        {
            Matrix3 other = obj as Matrix3;

            if (other != null)
            {
                return this.Equals(other, 0.0001);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Matrix3 other, double epsilon)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                for (int y = 0; y < 3; y++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        double diff = System.Math.Abs(_values[y, x] - other[y, x]);

                        if (diff > epsilon)
                        {
                            return false;
                        }
                    }
                }

                return true;
            }
        }

        public override int GetHashCode()
        {
            double hash = 0;

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    hash = (hash * 17.0) + _values[y, x];
                }
            }

            return (int)hash;
        }

        public double this[int x, int y]
        {
            get
            {
                return _values[x, y];
            }
        }

        #region Static Methods

        public static Matrix3 Transpose(Matrix3 mat)
        {
            if (mat == null)
            {
                throw new ArgumentNullException("mat");
            }

            double[,] transpose = new double[3, 3];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    transpose[x, y] = mat[y, x];
                }
            }

            return new Matrix3(transpose);
        }

        public static Matrix3 operator *(Matrix3 a, Matrix3 b)
        {
            double[,] result = new double[3, 3];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    result[y,x] = (a[y, 0] * b[0, x]) + (a[y, 1] * b[1, x]) + (a[y, 2] * b[2, x]);
                }
            }

            return new Matrix3(result);
        }

        public static Vector3 operator *(Vector3 a, Matrix3 b)
        {
            return new Vector3(
                (a.X * b[0, 0]) + (a.Y * b[1, 0]) + (a.Z * b[2, 0]),
                (a.X * b[0, 1]) + (a.Y * b[1, 1]) + (a.Z * b[2, 1]),
                (a.X * b[0, 2]) + (a.Y * b[1, 2]) + (a.Z * b[2, 2]));
        }

        public static Vector3 operator *(Matrix3 a, Vector3 b)
        {
            return new Vector3(
                (a[0, 0] * b.X) + (a[0, 1] * b.Y) + (a[0, 2] * b.Z),
                (a[1, 0] * b.X) + (a[1, 1] * b.Y) + (a[1, 2] * b.Z),
                (a[2, 0] * b.X) + (a[2, 1] * b.Y) + (a[2, 2] * b.Z)
                );
        }

        public static Matrix3 operator *(Matrix3 a, double b)
        {
            double[,] result = new double[3, 3];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    result[y, x] = a[y, x] * b;
                }
            }

            return new Matrix3(result);
        }

        public static Matrix3 operator *(double a, Matrix3 b)
        {
            return b * a;
        }



        public static Matrix3 operator /(Matrix3 a, double b)
        {
            if (b.Equals(0.0))
            {
                throw new DivideByZeroException();
            }

            double[,] result = new double[3, 3];

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    result[y, x] = a[y, x] / b;
                }
            }

            return new Matrix3(result);
        }

        public static Matrix3 operator +(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(new double[,]
            {
                {a[0,0] + b[0,0], a[0,1] + b[0,1], a[0,2] + b[0,2]},
                {a[1,0] + b[1,0], a[1,1] + b[1,1], a[1,2] + b[1,2]},
                {a[2,0] + b[2,0], a[2,1] + b[2,1], a[2,2] + b[2,2]},

            });
        }

        public static Matrix3 operator -(Matrix3 a, Matrix3 b)
        {
            return new Matrix3(new double[,]
            {
                {a[0,0] - b[0,0], a[0,1] - b[0,1], a[0,2] - b[0,2]},
                {a[1,0] - b[1,0], a[1,1] - b[1,1], a[1,2] - b[1,2]},
                {a[2,0] - b[2,0], a[2,1] - b[2,1], a[2,2] - b[2,2]},

            });
        }

        public static Matrix3 Identity
        {
            get
            {
                return new Matrix3(new double[,]
                {
                    {1.0,0.0,0.0}, {0.0,1.0,0.0}, {0.0,0.0,1.0}
                });
            }
        }

        #endregion

    }
}
