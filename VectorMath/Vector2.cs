using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Boyd.Math
{
    public class Vector2
    {
        private const double Epsilon = 0.0001;

        public double X { get; private set; }
        public double Y { get; private set; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double Magnitude()
        {
            return System.Math.Sqrt((X * X) + (Y * Y));
        }

        public Vector2 Normalize()
        {
            return Vector2.Normalize(this);
        }

        public bool IsNormal()
        {
            return (System.Math.Abs(this.Magnitude() - 1.0) < Epsilon);
        }

        public double Dot(Vector2 other)
        {
            return Vector2.Dot(this, other);
        }

        #region Static Members

        public static Vector2 Zero
        {
            get { return new Vector2(0.0, 0.0); }
        }

        public static Vector2 Up
        {
            get { return new Vector2(0.0, 1.0); }
        }

        public static Vector2 Right
        {
            get { return new Vector2(1.0, 0.0); }
        }

        public static double Dot(Vector2 a, Vector2 b)
        {
            return (a.X * b.X) + (a.Y * b.Y);
        }

        public static Vector2 Normalize(Vector2 vec)
        {
            double mag = vec.Magnitude();

            Vector2 result = new Vector2(vec.X / mag, vec.Y / mag);

            if (double.IsNaN(result.X) || double.IsNaN(result.Y))
            {
                throw new InvalidOperationException(ErrorStrings.NormalizeZeroVector);
            }
            else
            {
                return result;
            }
        }

        #endregion

        #region Operators

        public static Vector2 operator +(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X + b.X, a.Y + b.Y);
        }

        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return new Vector2(a.X - b.X, a.Y - b.Y);
        }

        public static Vector2 operator *(Vector2 a, double factor)
        {
            return new Vector2(a.X * factor, a.Y * factor);
        }

        public static Vector2 operator /(Vector2 a, double factor)
        {
            return new Vector2(a.X / factor, a.Y / factor);
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj)
        {
            Vector2 other = obj as Vector2;

            if (other != null)
            {
                return this.Equals(other, Epsilon);
            }

            return false;
        }

        public bool Equals(Vector2 other, double epsilon)
        {
            return (System.Math.Abs(this.X - other.X) < epsilon &&
                System.Math.Abs(this.Y - other.Y) < epsilon);
            
        }


        public override int GetHashCode()
        {
            int hash = (int)X;

            hash = (hash * 17) + (int)Y;

            return hash;
        }

        public override string ToString()
        {
            return string.Format("({0:0.###}, {1:0.###})", X, Y);
        }

        #endregion
    }
}
