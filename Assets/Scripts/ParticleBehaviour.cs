using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField]
    private Particle m_Particle;

    public Particle particle
    {
        get { return m_Particle; }
        set { m_Particle = value; }
    }

    // Use this for initialization
    void Start()
    {
        transform.position = m_Particle.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        transform.position = m_Particle.position;
    }

    public static ParticleBehaviour Create(GameObject baseGameObject = null)
    {
        // If a baseGameObject was passed in instantiate it, otherwise create a default
        var newGameObject =
            baseGameObject ?
            Instantiate(baseGameObject) : new GameObject();

        newGameObject.name = "New Particle";

        var newParticle = new Particle();
        var newParticleBehaviour = newGameObject.AddComponent<ParticleBehaviour>();

        newParticleBehaviour.particle = newParticle;

        return newParticleBehaviour;
    }
}
