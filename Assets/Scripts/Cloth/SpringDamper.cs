using System;
using System.Collections.Generic;

namespace Cloth
{
    [Serializable]
    public class SpringDamper
    {
        private float m_SpringConstant = 15f;

        private float m_DampingFactor = 5f;

        private float m_RestLength = 1.25f;

        private bool m_IsTorn;
        private float m_TearLength = 3.5f;

#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private Particle m_Head, m_Tail;

        private readonly List<ClothTriangle> m_ClothTriangles = new List<ClothTriangle>();

        public float restLength
        {
            get { return m_RestLength; }
        }

        public bool isTorn
        {
            get { return m_IsTorn; }
        }
        public float tearLength
        {
            get { return m_TearLength; }
            set { m_TearLength = value; }
        }

        public Particle head
        {
            get { return m_Head; }
            set { m_Head = value; }
        }
        public Particle tail
        {
            get { return m_Tail; }
            set { m_Tail = value; }
        }

        public List<ClothTriangle> clothTriangles
        {
            get { return m_ClothTriangles; }
        }

        public SpringDamper() { }
        public SpringDamper(Particle head, Particle tail)
        {
            m_Head = head;
            m_Tail = tail;

            m_RestLength = (m_Head.position - m_Tail.position).magnitude * 0.9f;
        }

        public void Update()
        {
            if (m_IsTorn)
                return;

            var displacement = m_Tail.position - m_Head.position;
            var distance = displacement.magnitude;

            if (distance >= m_RestLength * m_TearLength)
            {
                m_IsTorn = true;

                foreach (var clothTriangle in clothTriangles)
                    clothTriangle.isTorn = true;

                return;
            }

            var displacementDirection = displacement / distance;

            var headVel1D = displacementDirection * m_Head.velocity;
            var tailVel1D = displacementDirection * m_Tail.velocity;

            var linearSpringForce = -m_SpringConstant * (m_RestLength - distance);

            var linearDamping = -m_DampingFactor * (headVel1D - tailVel1D);

            var force = linearSpringForce + linearDamping;

            var headForce = force * displacementDirection;
            var tailForce = -headForce;

            m_Head.AddForce(headForce);
            m_Tail.AddForce(tailForce);

            return;
        }
    }
}
