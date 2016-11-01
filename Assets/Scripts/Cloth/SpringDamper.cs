using System;

using Utility.Vector;

namespace Cloth
{
    [Serializable]
    public class SpringDamper
    {
        private float m_SpringConstant = 50f;

        private float m_DampingFactor = 10f;

        private float m_RestLength = 1.25f;

#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private Particle m_Head, m_Tail;

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

        public SpringDamper() { }
        public SpringDamper(Particle head, Particle tail)
        {
            m_Head = head;
            m_Tail = tail;
        }

        public void Update()
        {
            var displacement = m_Tail.position - m_Head.position;
            var distance = displacement.magnitude;
            var displacementDirection = displacement / distance;

            var headVel1D = Vector3.Dot(displacementDirection, m_Head.velocity);
            var tailVel1D = Vector3.Dot(displacementDirection, m_Tail.velocity);

            var linearSpringForce = -m_SpringConstant * (m_RestLength - distance);

            var linearDamping = -m_DampingFactor * (headVel1D - tailVel1D);

            var springDamperForce = linearSpringForce + linearDamping;

            var headForce = springDamperForce * displacementDirection;
            var tailForce = -headForce;

            m_Head.AddForce(headForce);
            m_Tail.AddForce(tailForce);
        }
    }
}
