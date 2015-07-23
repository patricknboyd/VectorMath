using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boyd.Math
{
    /// <summary>
    /// Represents a 4 dimensional vector.
    /// </summary>
    public class Vector4
    {
        /// <summary>
        /// The default margin of error used to calculate vector equality.
        /// </summary>
        private const double Epsilon = 0.0001;

        /// <summary>
        /// Gets the X value of the vector.
        /// </summary>
        public double X { get; private set; }
        /// <summary>
        /// Gets the Y value of the vector.
        /// </summary>
        public double Y { get; private set; }
        /// <summary>
        /// Gets the Z value of the vector.
        /// </summary>
        public double Z { get; private set; }
        /// <summary>
        /// Gets the w value of the vector.
        /// </summary>
        /// <remarks>
        /// The homogenous coordinate for 3D graphics applications.
        /// </remarks>
        public double W { get; private set; }

        /// <summary>
        /// Creates a new zero Vector4.
        /// </summary>
        public Vector4()
            : this(0.0, 0.0, 0.0, 0.0)
        { }

        /// <summary>
        /// Creates a four dimensional vector with the given values, and W set to 1.0.
        /// </summary>
        /// <param name="x">The X value of the vector.</param>
        /// <param name="y">The Y value of the vector.</param>
        /// <param name="z">The Z value of the vector.</param>
        public Vector4(double x, double y, double z)
            : this(x, y, z, 1.0)
        { }

        /// <summary>
        /// Creates a four dimensional vector with the given values.
        /// </summary>
        /// <param name="x">The X value of the vector.</param>
        /// <param name="y">The Y value of the vector.</param>
        /// <param name="z">The Z value of the vector.</param>
        /// <param name="w">The w value of the vector.</param>
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        /// <summary>
        /// Checks to see if the vector is a unit vector.
        /// </summary>
        /// <returns><code>true</code> if the vector is a unit vector, otherwise <code>false</code>.</returns>
        /// <remarks>I should add the ability to specify the epsilon used on this.</remarks>
        public bool IsNormal()
        {
            return (System.Math.Abs(this.Magnitude() - 1.0) < Epsilon);
        }

        /// <summary>
        /// Creates a unit vector pointing in the same direction as this vector.
        /// </summary>
        /// <returns></returns>
        public Vector4 Normalize()
        {
            return Vector4.Normalize(this);
        }

        /// <summary>
        /// Calculates the dot product of this vector, with another vector.
        /// </summary>
        /// <param name="other">The other vector to calculate the dot product against.</param>
        /// <returns></returns>
        public double Dot(Vector4 other)
        {
            return Vector4.Dot(this, other);
        }

        /// <summary>
        ///  Gets the Vector3 representation of this vector scaled by the Homogenous coordinate.
        /// </summary>
        /// <returns></returns>
        public Vector3 ScaledVector()
        {
            return new Vector3(X / W, Y / W, Z / W);
        }

        public double Magnitude()
        {
            return System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z) + (W * W));
        }

        public override string ToString()
        {
            return string.Format("({0:0.###}, {1:0.###}, {2:0.###}, {3:0.###})", X, Y, Z, W);
        }

        public override bool Equals(object obj)
        {
            Vector4 other = obj as Vector4;

            if (other != null)
            {
                return this.Equals(other, Epsilon);
            }

            return false;
        }

        public bool Equals(Vector4 other, double epsilon)
        {
            return (
                System.Math.Abs(this.X - other.X) < epsilon &&
                System.Math.Abs(this.Y - other.Y) < epsilon &&
                System.Math.Abs(this.Z - other.Z) < epsilon &&
                System.Math.Abs(this.W - other.W) < epsilon
                );

        }

        public override int GetHashCode()
        {
            int hash = (int)X;

            hash = (hash * 17) + (int)Y;
            hash = (hash * 17) + (int)Z;
            hash = (hash * 17) + (int)W;

            return hash;
        }

        #region Static Methods

        public static Vector4 Zero { get { return new Vector4(); } }

        /// <summary>
        /// Creates a unit vector pointing in the same direction as the input vector.
        /// </summary>
        /// <param name="vec">The vector to normalize.</param>
        /// <returns>A unit vector pointing in the same direction as the input vector.</returns>
        public static Vector4 Normalize(Vector4 vec)
        {
            if (vec == null)
            {
                throw new ArgumentNullException("vec");
            }

            double mag = vec.Magnitude();

            Vector4 result = new Vector4(
                vec.X / mag,
                vec.Y / mag,
                vec.Z / mag,
                vec.W / mag);

            if (double.IsNaN(result.X) || double.IsNaN(result.Y) || double.IsNaN(result.Z) || double.IsNaN(result.W))
            {
                throw new InvalidOperationException(ErrorStrings.NormalizeZeroVector);
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// Calculates the dot product of the two input vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static double Dot(Vector4 a, Vector4 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W + b.W);
        }

        #endregion

        #region Operators

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X + b.X, a.Y + b.Y, a.Z + b.Z, a.W + b.W);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.X - b.X, a.Y - b.Y, a.Z - b.Z, a.W - b.W);
        }

        public static Vector4 operator *(Vector4 a, double b)
        {
            return new Vector4(a.X * b, a.Y * b, a.Z * b, a.W * b);
        }

        public static Vector4 operator /(Vector4 a, double b)
        {
            return new Vector4(a.X / b, a.Y / b, a.Z / b, a.W / b);
        }

        public static Vector4 operator *(double a, Vector4 b)
        {
            return (b * a);
        }

        #endregion

    }
}
