using System.Collections.Generic;

using UnityEngine;

namespace Cloth
{
    public class ClothSystemBehaviourFactory : MonoBehaviour
    {
        [SerializeField]
        private GameObject m_AgentPrefab;

        [Space, SerializeField]
        private Vector3 m_AgentNumber;
        [SerializeField]
        private float m_Separation;

        [Space, SerializeField]
        private Vector3 m_Gravity;

        // Use this for initialization
        private void Awake()
        {
            var newClothSystemBehaviour = ClothSystemBehaviour.Create();
            newClothSystemBehaviour.transform.position = transform.position;

            newClothSystemBehaviour.clothSystem.gravity = m_Gravity;

            var agentParent = new GameObject("Agents");
            agentParent.transform.SetParent(newClothSystemBehaviour.transform, false);

            var totalParticles = new List<Particle>();
            for (var i = 0f; i < m_AgentNumber.z; ++i)
            {
                for (var j = 0f; j < m_AgentNumber.y; ++j)
                {
                    for (var k = 0f; k < m_AgentNumber.x; ++k)
                    {
                        var newAgent = ParticleBehaviour.Create(m_AgentPrefab);

                        newAgent.transform.SetParent(agentParent.transform, false);
                        newAgent.transform.localPosition =
                            new Vector3(
                                k * m_Separation - (m_AgentNumber.x - 1) * m_Separation / 2,
                                (m_AgentNumber.y - 1) * m_Separation / 2 - j * m_Separation,
                                i * m_Separation - (m_AgentNumber.z - 1) * m_Separation / 2);

                        // Set agents position to the Unity GameObject's world position
                        newAgent.particle.position = newAgent.transform.position;

                        newClothSystemBehaviour.clothSystem.agents.Add(newAgent.particle);
                        totalParticles.Add(newAgent.particle);
                    }
                }
            }

            var springDamperParent = new GameObject("Spring Dampers");
            springDamperParent.transform.SetParent(newClothSystemBehaviour.transform, false);

            var totalSpringDampers = new List<SpringDamperBehaviour>();
            for (var i = 0; i < totalParticles.Count; ++i)
            {
                if (i + 1 < totalParticles.Count && (i + 1) % (int)m_AgentNumber.x != 0)
                {
                    var topLeftToTopRight = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + 1]);
                    topLeftToTopRight.transform.SetParent(
                        springDamperParent.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        topLeftToTopRight.springDamper);

                    totalSpringDampers.Add(topLeftToTopRight);
                }

                if (i + m_AgentNumber.x < totalParticles.Count)
                {
                    var topLeftToBottomLeft = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x]);
                    topLeftToBottomLeft.transform.SetParent(
                        springDamperParent.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        topLeftToBottomLeft.springDamper);

                    totalSpringDampers.Add(topLeftToBottomLeft);
                }

                if (i + m_AgentNumber.x + 1 < totalParticles.Count && (i + 1) % (int)m_AgentNumber.x != 0)
                {
                    var topRightToBottomLeft = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x + 1]);
                    topRightToBottomLeft.transform.SetParent(
                        springDamperParent.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        topRightToBottomLeft.springDamper);

                    totalSpringDampers.Add(topRightToBottomLeft);
                }

                if (i + m_AgentNumber.x - 1 < totalParticles.Count && i % (int)m_AgentNumber.x != 0)
                {
                    var topLeftToBottomRight = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x - 1]);
                    topLeftToBottomRight.transform.SetParent(
                        springDamperParent.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        topLeftToBottomRight.springDamper);

                    totalSpringDampers.Add(topLeftToBottomRight);
                }
            }

            var clothTriangleParent = new GameObject("Triangles");
            clothTriangleParent.transform.SetParent(newClothSystemBehaviour.transform, false);

            foreach (var springDamperHead in totalSpringDampers)
            {
                foreach (var springDamperMiddle in totalSpringDampers)
                {
                    if (springDamperMiddle == springDamperHead)
                        continue;

                    foreach (var springDamperTail in totalSpringDampers)
                    {
                        if (springDamperTail == springDamperHead || springDamperTail == springDamperMiddle)
                            continue;

                        if (springDamperTail.springDamper.head.position.y
                            <= springDamperMiddle.springDamper.head.position.y &&
                            springDamperTail.springDamper.head.position.x
                            < springDamperMiddle.springDamper.head.position.x &&
                            springDamperHead.springDamper.tail == springDamperMiddle.springDamper.head &&
                            springDamperMiddle.springDamper.tail == springDamperTail.springDamper.tail &&
                            springDamperHead.springDamper.head == springDamperTail.springDamper.head)
                        {
                            var newClothTriangle = ClothTriangleBehaviour.Create(
                                springDamperTail.springDamper.tail,
                                springDamperMiddle.springDamper.head,
                                springDamperHead.springDamper.head);
                            newClothTriangle.transform.SetParent(clothTriangleParent.transform, false);

                            newClothTriangle.clothTriangle.springDampers.Add(springDamperHead.springDamper);
                            newClothTriangle.clothTriangle.springDampers.Add(springDamperMiddle.springDamper);
                            newClothTriangle.clothTriangle.springDampers.Add(springDamperTail.springDamper);

                            springDamperHead.springDamper.clothTriangles.Add(newClothTriangle.clothTriangle);
                            springDamperMiddle.springDamper.clothTriangles.Add(
                                newClothTriangle.clothTriangle);
                            springDamperTail.springDamper.clothTriangles.Add(newClothTriangle.clothTriangle);


                            newClothSystemBehaviour.clothSystem.clothTriangles.Add(
                                newClothTriangle.clothTriangle);
                        }

                        if (springDamperHead.springDamper.head.position.x
                            < springDamperMiddle.springDamper.tail.position.x &&
                            springDamperHead.springDamper.tail == springDamperMiddle.springDamper.tail &&
                            springDamperMiddle.springDamper.head == springDamperTail.springDamper.head &&
                            springDamperHead.springDamper.head == springDamperTail.springDamper.tail)
                        {
                            var newClothTriangle = ClothTriangleBehaviour.Create(
                                springDamperHead.springDamper.head,
                                springDamperMiddle.springDamper.tail,
                                springDamperTail.springDamper.head);
                            newClothTriangle.transform.SetParent(clothTriangleParent.transform, false);

                            newClothTriangle.clothTriangle.springDampers.Add(springDamperHead.springDamper);
                            newClothTriangle.clothTriangle.springDampers.Add(springDamperMiddle.springDamper);
                            newClothTriangle.clothTriangle.springDampers.Add(springDamperTail.springDamper);

                            springDamperHead.springDamper.clothTriangles.Add(newClothTriangle.clothTriangle);
                            springDamperMiddle.springDamper.clothTriangles.Add(
                                newClothTriangle.clothTriangle);
                            springDamperTail.springDamper.clothTriangles.Add(newClothTriangle.clothTriangle);

                            newClothSystemBehaviour.clothSystem.clothTriangles.Add(
                                newClothTriangle.clothTriangle);
                        }
                    }
                }
            }
        }
    }
}
