using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Math
{
    /// <summary>
    /// Represents a 4x4 matrix.
    /// </summary>
    public class Matrix4
    {
        private double[,] _values;
        private const int Width = 4;
        private const int Height = 4;

        /// <summary>
        /// Creates a new zero matrix.
        /// </summary>
        public Matrix4()
        {
            _values = new double[,]
            {
                {0.0, 0.0, 0.0, 0.0},
                {0.0, 0.0, 0.0, 0.0},
                {0.0, 0.0, 0.0, 0.0},
                {0.0, 0.0, 0.0, 0.0}
            };
        }

        /// <summary>
        /// Create a 4x4 matrix with the given values.
        /// </summary>
        /// <param name="values"></param>
        public Matrix4(double[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length > 16)
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

        public Matrix4(double[,] values)
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

        public Matrix4(Vector4[] values)
        {
            if (values == null)
            {
                throw new ArgumentNullException("values");
            }
            if (values.Length > 4)
            {
                throw new ArgumentException(ErrorStrings.TooManyValuesForMatrix);
            }

            _values = new double[Height, Width];

            for (int y = 0; y < values.Length; y++)
            {
                if (values[y] == null)
                {
                    throw new InvalidOperationException("Null value in input array.");
                }

                _values[y, 0] = values[y].X;
                _values[y, 1] = values[y].Y;
                _values[y, 2] = values[y].Z;
                _values[y, 3] = values[y].W;
            }

            for (int i = values.Length; i < Height; i++)
            {
                _values[i, 0] = 0.0;
                _values[i, 1] = 0.0;
                _values[i, 2] = 0.0;
                _values[i, 3] = 0.0;
            }
        }

        public double this[int x, int y]
        {
            get
            {
                return _values[x, y];
            }
        }

        /// <summary>
        /// Calculates the transpose of this matrix.
        /// </summary>
        /// <returns>A matrix that is the tranpose of this matrix.</returns>
        public Matrix4 Transpose()
        {
            return Matrix4.Transpose(this);
        }

        public double Determinant()
        {
            double determ =
                (_values[0, 0] * _values[1, 1] * _values[2, 2] * _values[3, 3]) +
                (_values[0, 1] * _values[1, 2] * _values[2, 3] * _values[3, 0]) +
                (_values[0, 2] * _values[1, 3] * _values[2, 0] * _values[3, 1]) +
                (_values[0, 3] * _values[1, 0] * _values[2, 1] * _values[3, 2]);

            determ -=
                (_values[0, 0] * _values[1, 3] * _values[2, 2] * _values[3, 1]) +
                (_values[0, 1] * _values[1, 0] * _values[2, 3] * _values[3, 2]) +
                (_values[0, 2] * _values[1, 1] * _values[2, 0] * _values[3, 3]) +
                (_values[0, 3] * _values[1, 2] * _values[2, 1] * _values[3, 0]);

            return determ;
        }

        public Matrix4 Inverse()
        {
            throw new NotImplementedException();
        }

        #region Static Methods

        /// <summary>
        /// Calculates the transpose of the input matrix.
        /// </summary>
        /// <returns>A matrix that is the tranpose of the input matrix.</returns>
        public static Matrix4 Transpose(Matrix4 mat)
        {
            if (mat == null)
            {
                throw new ArgumentNullException("mat");
            }

            double[,] transpose = new double[4, 4];

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    transpose[x, y] = mat[y, x];
                }
            }

            return new Matrix4(transpose);
        }

        /// <summary>
        /// Returns the 4x4 Identity Matrix.
        /// </summary>
        public static Matrix4 Identity
        {
            get
            {
                return new Matrix4(new double[,]
                {
                    {1.0,0.0,0.0,0.0}, {0.0,1.0,0.0,0.0}, {0.0,0.0,1.0,0.0}, {0.0,0.0,0.0,1.0}
                });
            }
        }

        #endregion

        #region Operators

        public static Matrix4 operator *(Matrix4 a, Matrix4 b)
        {
            double[,] result = new double[Width, Height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    result[y, x] = (a[y, 0] * b[0, x]) + (a[y, 1] * b[1, x]) + (a[y, 2] * b[2, x]) + (a[y, 3] * b[3, x]);
                }
            }

            return new Matrix4(result);
        }

        public static Vector4 operator *(Vector4 a, Matrix4 b)
        {
            return new Vector4(
                (a.X * b[0, 0]) + (a.Y * b[1, 0]) + (a.Z * b[2, 0]) + (a.W * b[3, 0]),
                (a.X * b[0, 1]) + (a.Y * b[1, 1]) + (a.Z * b[2, 1]) + (a.W * b[3, 1]),
                (a.X * b[0, 2]) + (a.Y * b[1, 2]) + (a.Z * b[2, 2]) + (a.W * b[3, 2]),
                (a.X * b[0, 3]) + (a.Y * b[1, 3]) + (a.Z * b[2, 3]) + (a.W * b[3, 3])
                );
        }

        public static Vector4 operator *(Matrix4 a, Vector4 b)
        {
            return new Vector4(
                (a[0, 0] * b.X) + (a[0, 1] * b.Y) + (a[0, 2] * b.Z) + (a[0, 3] * b.W),
                (a[1, 0] * b.X) + (a[1, 1] * b.Y) + (a[1, 2] * b.Z) + (a[1, 3] * b.W),
                (a[2, 0] * b.X) + (a[2, 1] * b.Y) + (a[2, 2] * b.Z) + (a[2, 3] * b.W),
                (a[3, 0] * b.X) + (a[3, 1] * b.Y) + (a[3, 2] * b.Z) + (a[3, 3] * b.W)
                );
        }

        public static Matrix4 operator *(Matrix4 a, double b)
        {
            double[,] result = new double[4, 4];

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    result[y, x] = a[y, x] * b;
                }
            }

            return new Matrix4(result);
        }

        public static Matrix4 operator *(double a, Matrix4 b)
        {
            return b * a;
        }

        public static Matrix4 operator /(Matrix4 a, double b)
        {
            if (b.Equals(0.0))
            {
                throw new DivideByZeroException();
            }

            double[,] result = new double[4, 4];

            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 4; x++)
                {
                    result[y, x] = a[y, x] / b;
                }
            }

            return new Matrix4(result);
        }

        public static Matrix4 operator +(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(new double[,]
            {
                {a[0,0] + b[0,0], a[0,1] + b[0,1], a[0,2] + b[0,2], a[0,3] + b[0,3]},
                {a[1,0] + b[1,0], a[1,1] + b[1,1], a[1,2] + b[1,2], a[1,3] + b[1,3]},
                {a[2,0] + b[2,0], a[2,1] + b[2,1], a[2,2] + b[2,2], a[2,3] + b[2,3]},
                {a[3,0] + b[3,0], a[3,1] + b[3,1], a[3,2] + b[3,2], a[3,3] + b[3,3]}

            });
        }

        public static Matrix4 operator -(Matrix4 a, Matrix4 b)
        {
            return new Matrix4(new double[,]
            {
                {a[0,0] - b[0,0], a[0,1] - b[0,1], a[0,2] - b[0,2], a[0,3] - b[0,3]},
                {a[1,0] - b[1,0], a[1,1] - b[1,1], a[1,2] - b[1,2], a[1,3] - b[1,3]},
                {a[2,0] - b[2,0], a[2,1] - b[2,1], a[2,2] - b[2,2], a[2,3] - b[2,3]},
                {a[3,0] - b[3,0], a[3,1] - b[3,1], a[3,2] - b[3,2], a[3,3] - b[3,3]}

            });
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            for (int y = 0; y < Height; y++)
            {
                output.AppendFormat("[{0:0.###}, {1:0.###}, {2:0.###}, {3:0.###}]", _values[y, 0], _values[y, 1], _values[y, 2], _values[y, 3]);
                output.AppendLine();
            }

            return output.ToString();
        }

        public override bool Equals(object obj)
        {
            Matrix4 other = obj as Matrix4;

            if (other != null)
            {
                return this.Equals(other, 0.0001);
            }
            else
            {
                return false;
            }
        }

        public bool Equals(Matrix4 other, double epsilon)
        {
            if (other == null)
            {
                return false;
            }
            else
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int x = 0; x < Width; x++)
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

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    hash = (hash * 17.0) + _values[y, x];
                }
            }

            return (int)hash;
        }

        #endregion
    }
}
