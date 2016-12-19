using System;

namespace Utility
{
    namespace Vector
    {
        [Serializable]
        public struct Vector3
        {
#if UNITY_5
            [UnityEngine.SerializeField]
#endif
            public float x, y, z;

            public static Vector3 zero = new Vector3();

            public float magnitude
            {
                get { return (float)Math.Sqrt(x * x + y * y + z * z); }
            }
            public Vector3 normalized
            {
                get
                {
                    var cachedMagnitude = magnitude;
                    if(cachedMagnitude == 0f)
                        return zero;

                    return new Vector3(x / cachedMagnitude, y / cachedMagnitude, z / cachedMagnitude);
                }
            }

            public Vector3(float x, float y, float z = 0f)
            {
                this.x = x;
                this.y = y;
                this.z = z;
            }
            public Vector3(Vector2 a, float z = 0f)
            {
                this.x = a.x;
                this.y = a.y;
                this.z = z;
            }

            public static float Dot(Vector3 a, Vector3 b)
            {
                return a.x * b.x + a.y * b.y + a.z * b.z;
            }
            public static Vector3 Cross(Vector3 a, Vector3 b)
            {
                return
                    new Vector3(
                        a.y * b.z - a.z * b.y,
                        a.z * b.x - a.x * b.z,
                        a.x * b.y - a.y * b.x);
            }

#if UNITY_5
            public static implicit operator UnityEngine.Vector3(Vector3 a)
            {
                return new UnityEngine.Vector3(a.x, a.y, a.z);
            }
            public static implicit operator Vector3(UnityEngine.Vector3 a)
            {
                return new Vector3(a.x, a.y, a.z);
            }
#endif

            public static Vector3 operator -(Vector3 a)
            {
                return new Vector3(-a.x, -a.y, -a.z);
            }

            public static Vector3 operator +(Vector3 a, Vector3 b)
            {
                return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
            }
            public static Vector3 operator +(Vector3 a, float b)
            {
                return new Vector3(a.x + b, a.y + b, a.z + b);
            }
            public static Vector3 operator +(float a, Vector3 b)
            {
                return new Vector3(a + b.x, a + b.y, a + b.z);
            }

            public static Vector3 operator -(Vector3 a, Vector3 b)
            {
                return a + -b;
            }
            public static Vector3 operator -(Vector3 a, float b)
            {
                return a + -b;
            }
            public static Vector3 operator -(float a, Vector3 b)
            {
                return a + -b;
            }

            public static float operator *(Vector3 a, Vector3 b)
            {
                return Dot(a, b);
            }
            public static Vector3 operator *(Vector3 a, float b)
            {
                return new Vector3(a.x * b, a.y * b, a.z * b);
            }
            public static Vector3 operator *(float a, Vector3 b)
            {
                return new Vector3(a * b.x, a * b.y, a * b.z);
            }

            public static Vector3 operator /(Vector3 a, float b)
            {
                return new Vector3(a.x / b, a.y / b, a.z / b);
            }
        }
    }
}