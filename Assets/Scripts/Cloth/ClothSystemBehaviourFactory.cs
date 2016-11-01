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

        [Space, SerializeField]
        private Vector3 m_Gravity;

        // Use this for initialization
        private void Start()
        {
            var newClothSystemBehaviour = ClothSystemBehaviour.Create();
            newClothSystemBehaviour.transform.position = transform.position;

            newClothSystemBehaviour.clothSystem.gravity = m_Gravity;

            var totalParticles = new List<Particle>();
            for (var i = 0f; i < m_AgentNumber.z; ++i)
            {
                for (var j = 0f; j < m_AgentNumber.y; ++j)
                {
                    for (var k = 0f; k < m_AgentNumber.x; ++k)
                    {
                        var newAgent = ParticleBehaviour.Create(m_AgentPrefab);

                        newAgent.transform.SetParent(newClothSystemBehaviour.transform, false);
                        newAgent.transform.localPosition =
                            new Vector3(
                                k,
                                j,
                                i);

                        // Set agents position to the Unity GameObject's world position
                        newAgent.particle.position = newAgent.transform.position;

                        newClothSystemBehaviour.clothSystem.agents.Add(newAgent.particle);
                        totalParticles.Add(newAgent.particle);
                    }
                }
            }

            for (var i = 0; i < totalParticles.Count; ++i)
            {
                if (i + 1 < totalParticles.Count && (i + 1) % (int)m_AgentNumber.x != 0)
                {
                    var newSpringDamperBehaviour = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + 1]);
                    newSpringDamperBehaviour.transform.SetParent(
                        newClothSystemBehaviour.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        newSpringDamperBehaviour.springDamper);
                }

                if (i + m_AgentNumber.x < totalParticles.Count)
                {
                    var newSpringDamperBehaviour = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x]);
                    newSpringDamperBehaviour.transform.SetParent(
                        newClothSystemBehaviour.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        newSpringDamperBehaviour.springDamper);
                }

                if (i + m_AgentNumber.x + 1 < totalParticles.Count && (i + 1) % (int)m_AgentNumber.x != 0)
                {
                    var newSpringDamperBehaviour = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x + 1]);
                    newSpringDamperBehaviour.transform.SetParent(
                        newClothSystemBehaviour.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        newSpringDamperBehaviour.springDamper);
                }

                if (i + m_AgentNumber.x - 1 < totalParticles.Count && i % (int)m_AgentNumber.x != 0)
                {
                    var newSpringDamperBehaviour = SpringDamperBehaviour.Create(
                        totalParticles[i],
                        totalParticles[i + (int)m_AgentNumber.x - 1]);
                    newSpringDamperBehaviour.transform.SetParent(
                        newClothSystemBehaviour.transform,
                        false);

                    newClothSystemBehaviour.clothSystem.springDampers.Add(
                        newSpringDamperBehaviour.springDamper);
                }
            }
        }
    }
}
