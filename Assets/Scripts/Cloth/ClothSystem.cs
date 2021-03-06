﻿using System;
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
                springDamper.Update();

            foreach (var clothTriangle in m_ClothTriangles)
                clothTriangle.Update(1f);

            foreach (var agent in m_Agents)
            {
                if (agent.position.x < -2f)
                {
                    agent.position = new Vector3(-2f, agent.position.y, agent.position.z);
                    agent.velocity =
                        new Vector3(
                            -agent.velocity.x * 0.5f,
                            agent.velocity.y * 0.75f,
                            agent.velocity.z * 0.75f);
                }
                if (agent.position.x > 2f)
                {
                    agent.position = new Vector3(2f, agent.position.y, agent.position.z);
                    agent.velocity =
                        new Vector3(
                            -agent.velocity.x * 0.5f,
                            agent.velocity.y * 0.75f,
                            agent.velocity.z * 0.75f);
                }

                if (agent.position.y < -1.5f)
                {
                    agent.position = new Vector3(agent.position.x, -1.5f, agent.position.z);
                    agent.velocity =
                        new Vector3(
                            agent.velocity.x * 0.75f,
                            -agent.velocity.y * 0.5f,
                            agent.velocity.z * 0.75f);
                }
                if (agent.position.y > 1.5f)
                {
                    agent.position = new Vector3(agent.position.x, 1.5f, agent.position.z);
                    agent.velocity =
                        new Vector3(
                            agent.velocity.x * 0.75f,
                            -agent.velocity.y * 0.5f,
                            agent.velocity.z * 0.75f);
                }

                if (agent.velocity.magnitude > 15f)
                    agent.velocity = agent.velocity * 15f;

                agent.Update(deltaTime);
            }
        }
    }
}
