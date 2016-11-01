using System;

namespace Utility
{
    namespace Vector
    {
        [Serializable]
        public struct Vector2
        {
#if UNITY_5
            [UnityEngine.SerializeField]
#endif
            public float x, y;

            public static Vector2 zero = new Vector2();

            public float magnitude
            {
                get { return (float)Math.Sqrt(x * x + y * y); }
            }
            public Vector2 normalized
            {
                get
                {
                    var cachedMagnitude = magnitude;
                    return new Vector2(x / cachedMagnitude, y / cachedMagnitude);
                }
            }

            public Vector2(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public static float Dot(Vector2 a, Vector2 b)
            {
                return a.x * b.x + a.y * b.y;
            }

#if UNITY_5
            public static implicit operator UnityEngine.Vector2(Vector2 a)
            {
                return new UnityEngine.Vector2(a.x, a.y);
            }
            public static implicit operator Vector2(UnityEngine.Vector2 a)
            {
                return new Vector2(a.x, a.y);
            }
#endif

            public static Vector2 operator -(Vector2 a)
            {
                return new Vector2(-a.x, -a.y);
            }

            public static Vector2 operator +(Vector2 a, Vector2 b)
            {
                return new Vector2(a.x + b.x, a.y + b.y);
            }
            public static Vector2 operator +(Vector2 a, float b)
            {
                return new Vector2(a.x + b, a.y + b);
            }
            public static Vector2 operator +(float a, Vector2 b)
            {
                return new Vector2(a + b.x, a + b.y);
            }

            public static Vector2 operator -(Vector2 a, Vector2 b)
            {
                return a + -b;
            }
            public static Vector2 operator -(Vector2 a, float b)
            {
                return a + -b;
            }
            public static Vector2 operator -(float a, Vector2 b)
            {
                return a + -b;
            }

            public static float operator *(Vector2 a, Vector2 b)
            {
                return Dot(a, b);
            }
            public static Vector2 operator *(Vector2 a, float b)
            {
                return new Vector2(a.x * b, a.y * b);
            }
            public static Vector2 operator *(float a, Vector2 b)
            {
                return new Vector2(a * b.x, a * b.y);
            }

            public static Vector2 operator /(Vector2 a, float b)
            {
                return new Vector2(a.x / b, a.y / b);
            }
        }
    }
}
