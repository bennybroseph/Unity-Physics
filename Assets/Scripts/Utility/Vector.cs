using System;

#if UNITY_5
using UnityEngine;
#endif

namespace Utility
{
    [Serializable]
    public struct Vector
    {
#if UNITY_5
        [SerializeField]
#endif
        public float x, y, z;

        public Vector normalized
        {
            get
            {
                var thisMagnitude = magnitude;
                if (thisMagnitude == 0f)
                    return new Vector();

                return new Vector(x / thisMagnitude, y / thisMagnitude, z / thisMagnitude);
            }
        }

        public float magnitude
        {
            get { return (float)Math.Sqrt(x * x + y * y + z * z); }
        }

        public Vector(float x, float y, float z = 0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

#if UNITY_5
        public static implicit operator Vector3(Vector self)
        {
            return new Vector3(self.x, self.y, self.z);
        }
        public static implicit operator Vector(Vector3 self)
        {
            return new Vector(self.x, self.y, self.z);
        }
#endif

        public Vector Abs()
        {
            return new Vector(Math.Abs(x), Math.Abs(y), Math.Abs(z));
        }

        private bool Equals(Vector other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;

            return Equals((Vector)obj);
        }
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = x.GetHashCode();
                hashCode = (hashCode * 397) ^ y.GetHashCode();
                hashCode = (hashCode * 397) ^ z.GetHashCode();

                return hashCode;
            }
        }

        public static Vector operator -(Vector a)
        {
            return new Vector(-a.x, -a.y, -a.z);
        }

        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.x + b.x, a.y + b.y, a.z + b.z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return a + -b;
        }

        public static Vector operator +(Vector a, float b)
        {
            return new Vector(a.x + b, a.y + b, a.z + b);
        }
        public static Vector operator -(Vector a, float b)
        {
            return a + -b;
        }

        public static Vector operator *(Vector a, Vector b)
        {
            return new Vector(a.x * b.x, a.y * b.y, a.z * b.z);
        }
        public static Vector operator /(Vector a, Vector b)
        {
            return new Vector(a.x / b.x, a.y / b.y, a.z / b.z);
        }

        public static Vector operator *(Vector a, float b)
        {
            return new Vector(a.x * b, a.y * b, a.z * b);
        }
        public static Vector operator /(Vector a, float b)
        {
            return new Vector(a.x / b, a.y / b, a.z / b);
        }

        public static bool operator ==(Vector a, Vector b)
        {
            if (a == null && b == null)
                return true;

            if (a != null && b != null)
                return
                    Math.Abs(a.x - b.x) < float.Epsilon &&
                    Math.Abs(a.y - b.y) < float.Epsilon &&
                    Math.Abs(a.z - b.z) < float.Epsilon;

            return false;
        }
        public static bool operator !=(Vector a, Vector b)
        {
            return !(a == b);
        }
    }
}
