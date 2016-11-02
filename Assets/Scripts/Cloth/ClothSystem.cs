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
        private readonly List<ClothTriangle> m_ClothTriangles = new List<ClothTriangle>();

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
        public List<ClothTriangle> clothTriangles
        {
            get { return m_ClothTriangles; }
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
            {
                if (!springDamper.Update())
                    continue;

                // if the spring tore this frame we need to tear the triangle
                foreach (var clothTriangle in m_ClothTriangles)
                {
                    var matches = 0;

                    if (clothTriangle.particle1 == springDamper.tail &&
                        clothTriangle.particle2 == springDamper.head)
                        matches++;

                    if (clothTriangle.particle2 == springDamper.tail &&
                        clothTriangle.particle3 == springDamper.head)
                        matches++;

                    if (clothTriangle.particle3 == springDamper.tail &&
                        clothTriangle.particle1 == springDamper.head)
                        matches++;

                    if (matches > 0)
                        clothTriangle.isTorn = true;
                }
            }

            foreach (var clothTriangle in m_ClothTriangles)
                clothTriangle.Update(1f);

            foreach (var agent in m_Agents)
                agent.Update(deltaTime);
        }
    }
}
