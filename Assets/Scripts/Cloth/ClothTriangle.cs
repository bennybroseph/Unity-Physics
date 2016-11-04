using System;
using System.Collections.Generic;

using Utility.Vector;

namespace Cloth
{
    [Serializable]
    public class ClothTriangle
    {
#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private float m_Drag = 1f;

#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private Particle m_Particle1, m_Particle2, m_Particle3;

        private readonly List<SpringDamper> m_SpringDampers = new List<SpringDamper>();

        private Vector3 m_Normal;

        private bool m_IsTorn;

        public Particle particle1
        {
            get { return m_Particle1; }
            set { m_Particle1 = value; }
        }
        public Particle particle2
        {
            get { return m_Particle2; }
            set { m_Particle2 = value; }
        }
        public Particle particle3
        {
            get { return m_Particle3; }
            set { m_Particle3 = value; }
        }

        public List<SpringDamper> springDampers
        {
            get { return m_SpringDampers; }
        }

        public bool isTorn
        {
            get { return m_IsTorn; }
            set { m_IsTorn = value; }
        }

        public Vector3 normal
        {
            get { return m_Normal; }
        }

        public void Update(float density)
        {
            if (m_IsTorn)
                return;

            CalculateNormals();

            var a0 =
                0.5f * Vector3.Cross(
                    particle2.position - particle1.position,
                    particle3.position - particle1.position).magnitude;


            var vSurface = (particle1.velocity + particle2.velocity + particle3.velocity) / 3f;

            var v = vSurface - new Vector3(0f, 0f, 2f);

            var a = a0 * (v * m_Normal / v.magnitude);

            //var nStar =
            //    Vector3.Cross(
            //        particle2.position - particle1.position,
            //        particle3.position - particle1.position);

            //var van = ((v.magnitude * (v * nStar)) / (2 * nStar.magnitude)) * nStar;

            var aero = -0.5f * density * (v.magnitude * v.magnitude) * m_Drag * a * m_Normal;

            particle1.AddForce(aero / 3f);
            particle2.AddForce(aero / 3f);
            particle3.AddForce(aero / 3f);
        }

        public void CalculateNormals()
        {
            m_Normal =
                Vector3.Cross(
                    m_Particle2.position - m_Particle1.position,
                    m_Particle3.position - m_Particle1.position) /
                Vector3.Cross(
                    m_Particle2.position - m_Particle1.position,
                    m_Particle3.position - m_Particle1.position).magnitude;
        }
    }
}
