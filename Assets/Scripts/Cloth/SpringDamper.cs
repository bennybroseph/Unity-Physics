using System;

using Utility.Vector;

namespace Cloth
{
    [Serializable]
    public class SpringDamper
    {
        private float m_SpringConstant = 750f;

        private float m_DampingFactor = 5f;

        private float m_RestLength = 1.25f;

        private bool m_IsTorn;
        private float m_TearLength = 3.5f;

#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private Particle m_Head, m_Tail;

        public float restLength
        {
            get { return m_RestLength; }
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

        public bool isTorn
        {
            get { return m_IsTorn; }
        }
        public float tearLength
        {
            get { return m_TearLength; }
            set { m_TearLength = value; }
        }

        public SpringDamper() { }
        public SpringDamper(Particle head, Particle tail)
        {
            m_Head = head;
            m_Tail = tail;

            m_RestLength = (m_Head.position - m_Tail.position).magnitude * 0.85f;
        }

        public bool Update()
        {
            if (m_IsTorn)
                return false;

            var displacement = m_Tail.position - m_Head.position;
            var distance = displacement.magnitude;

            if (distance >= m_RestLength * m_TearLength)
            {
                m_IsTorn = true;
                return true;
            }

            var displacementDirection = displacement / distance;

            var headVel1D = Vector3.Dot(displacementDirection, m_Head.velocity);
            var tailVel1D = Vector3.Dot(displacementDirection, m_Tail.velocity);

            var linearSpringForce = -m_SpringConstant * (m_RestLength - distance);

            var linearDamping = -m_DampingFactor * (headVel1D - tailVel1D);

            var force = linearSpringForce + linearDamping;

            var headForce = force * displacementDirection;
            var tailForce = -headForce;

            m_Head.AddForce(headForce);
            m_Tail.AddForce(tailForce);

            return false;
        }
    }
}
