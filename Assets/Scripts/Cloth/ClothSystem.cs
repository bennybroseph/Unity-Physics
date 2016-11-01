using System;
using System.Collections.Generic;

using Utility.Vector;

namespace Cloth
{
    [Serializable]
    public class ClothSystem
    {
#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private readonly List<Particle> m_Agents = new List<Particle>();
#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private readonly List<SpringDamper> m_SpringDampers = new List<SpringDamper>();

#if UNITY_5
        [UnityEngine.SerializeField]
#endif
        private Vector3 m_Gravity;

        public List<Particle> agents
        {
            get { return m_Agents; }
        }
        public List<SpringDamper> springDampers
        {
            get { return m_SpringDampers; }
        }

        public Vector3 gravity
        {
            get { return m_Gravity; }
            set { m_Gravity = value; }
        }

        public void Update(float deltaTime)
        {
            foreach (var agent in m_Agents)
            {
                agent.force = Vector3.zero;
                agent.AddForce(m_Gravity);
            }

            foreach (var springDamper in m_SpringDampers)
                springDamper.Update();

            foreach (var agent in m_Agents)
            {
                agent.AddForce(m_Gravity);
                agent.Update(deltaTime);
            }
        }
    }
}
