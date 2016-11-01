using UnityEngine;



public class BoidBehaviour : MonoBehaviour
{
    [SerializeField]
    private Boid m_Boid;

    // Use this for initialization
    void Start()
    {
        m_Boid = new Boid(
            new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z));
    }

    public bool Init(Boid newBoid)
    {
        if (newBoid == null)
            return false;

        m_Boid = newBoid;
        transform.position = m_Boid.position;

        return true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = m_Boid.position;

        transform.forward = m_Boid.velocity;
    }
}
