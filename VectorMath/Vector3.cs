using System;

namespace Boyd.Math
{
    /// <summary>
    /// Represents a 3 dimensional vector.
    /// </summary>
    public class Vector3
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
        /// Creates a new zero vector.
        /// </summary>
        public Vector3()
            : this(0.0, 0.0, 0.0)
        { }

        /// <summary>
        /// Creates a new vector with the given values.
        /// </summary>
        /// <param name="x">The x value of the vector.</param>
        /// <param name="y">The y value of the vector.</param>
        /// <param name="z">The z value of the vector.</param>
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Creates a unit vector pointing in the same direction as this vector.
        /// </summary>
        /// <returns></returns>
        public Vector3 Normalize()
        {
            return Vector3.Normalize(this);
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

        public override string ToString()
        {
            return string.Format("({0:0.###}, {1:0.###}, {2:0.###})", X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            Vector3 other = obj as Vector3;

            if (other != null)
            {
                return this.Equals(other, Epsilon);
            }

            return false;
        }

        public bool Equals(Vector3 other, double epsilon)
        {
            return (System.Math.Abs(this.X - other.X) < epsilon &&
                System.Math.Abs(this.Y - other.Y) < epsilon &&
                System.Math.Abs(this.Z - other.Z) < epsilon);

        }

        public override int GetHashCode()
        {
            int hash = (int)X;

            hash = (hash * 17) + (int)Y;
            hash = (hash * 17) + (int)Z;

            return hash;
        }

        /// <summary>
        /// Calculates the dot product of this vector, with another vector.
        /// </summary>
        /// <param name="other">The other vector to calculate the dot product against.</param>
        /// <returns></returns>
        public double Dot(Vector3 other)
        {
            return Vector3.Dot(this, other);
        }

        /// <summary>
        /// Calculates the cross product of this vector, with another vector.
        /// </summary>
        /// <param name="other">The second vector in the cross product calculation.</param>
        /// <returns></returns>
        public Vector3 Cross(Vector3 other)
        {
            return Vector3.Cross(this, other);
        }



        /// <summary>
        /// Calculates the magnitude of the vector.
        /// </summary>
        /// <remarks>Pop, pop!</remarks>
        /// <returns></returns>
        public double Magnitude()
        {
            return System.Math.Sqrt((X * X) + (Y * Y) + (Z * Z));
        }

        #region Static Methods

        /// <summary>
        /// A vector of value (0,0,0).
        /// </summary>
        public static Vector3 Zero { get { return new Vector3(); } }
        /// <summary>
        /// A vector of value (0,1,0).
        /// </summary>
        public static Vector3 Up { get { return new Vector3(0.0, 1.0, 0.0); } }
        /// <summary>
        /// A vector of value (1,0,0).
        /// </summary>
        public static Vector3 Right { get { return new Vector3(1.0, 0.0, 0.0); } }
        /// <summary>
        /// A vector of value (0,0,1).
        /// </summary>
        public static Vector3 Forward { get { return new Vector3(0.0, 0.0, 1.0); } }

        /// <summary>
        /// Creates a unit vector pointing in the same direction as the input vector.
        /// </summary>
        /// <param name="vec">The vector to normalize.</param>
        /// <returns>A unit vector pointing in the same direction as the input vector.</returns>
        public static Vector3 Normalize(Vector3 vec)
        {
            if (vec == null)
            {
                throw new ArgumentNullException("vec");
            }

            double magnitude = vec.Magnitude();

           Vector3 result = new Vector3(vec.X / magnitude, vec.Y / magnitude, vec.Z / magnitude);

           if (double.IsNaN(result.X) || double.IsNaN(result.Y) || double.IsNaN(result.Z))
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
        public static double Dot(Vector3 a, Vector3 b)
        {
            return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z);
        }

        /// <summary>
        /// Calculates the cross product of the two input vectors.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(
                (a.Y * b.Z) - (a.Z * b.Y),
                -((a.X * b.Z) - (a.Z * b.X)),
                (a.X * b.Y) - (a.Y * b.X));
        }

        #endregion

        #region Operators

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator *(Vector3 a, double b)
        {
            return new Vector3(a.X * b, a.Y * b, a.Z * b);
        }

        public static Vector3 operator /(Vector3 a, double b)
        {
            return new Vector3(a.X / b, a.Y / b, a.Z / b);
        }

        public static Vector3 operator *(double a, Vector3 b)
        {
            return (b * a);
        }

        #endregion
    }
}
