using UnityEngine;
using UnityEngine.Events;

namespace Cloth
{
    public class ClothSystemBehaviour : MonoBehaviour
    {
        [SerializeField]
        private ClothSystem m_ClothSystem;

        [SerializeField]
        private bool m_IsPaused = true;
        [SerializeField]
        private readonly OnPauseEvent m_OnPauseEvent = new OnPauseEvent();

        public ClothSystem clothSystem
        {
            get { return m_ClothSystem; }
        }

        public bool isPaused
        {
            get { return m_IsPaused; }
            private set
            {
                m_IsPaused = value;
                m_OnPauseEvent.Invoke(m_IsPaused);
            }
        }

        public OnPauseEvent onPauseEvent
        {
            get { return m_OnPauseEvent; }
        }

        // Update is called once per frame
        private void FixedUpdate()
        {
            if (!m_IsPaused)
                m_ClothSystem.Update(Time.deltaTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                isPaused = !m_IsPaused;

            var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitCollider;
            if (Physics.Raycast(mouseRay, out hitCollider))
            {
                var particleBehaviour = hitCollider.collider.gameObject.GetComponent<ParticleBehaviour>();
                if (particleBehaviour)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        particleBehaviour.moveWithMouse = true;

                        particleBehaviour.wasKinematic = particleBehaviour.particle.isKinematic;
                        particleBehaviour.particle.isKinematic = true;
                    }
                    if (!Input.GetMouseButton(0) && Input.GetKeyDown(KeyCode.P))
                        particleBehaviour.particle.isKinematic = !particleBehaviour.particle.isKinematic;
                }
            }
        }

        public static ClothSystemBehaviour Create()
        {
            var newGameObject = new GameObject { name = "New Cloth System" };

            var newClothSystem = new ClothSystem();
            var newClothSystemBehaviour = newGameObject.AddComponent<ClothSystemBehaviour>();

            newClothSystemBehaviour.m_ClothSystem = newClothSystem;

            return newClothSystemBehaviour;
        }
    }

    public class OnPauseEvent : UnityEvent<bool> { }
}