using UnityEngine;
using System.Collections;

using Utility;

public class BoidBehaviour : MonoBehaviour
{
    [SerializeField]
    private Boid m_Boid;

    // Use this for initialization
    void Start()
    {
        m_Boid = new Boid(
            new Vector(
                transform.position.x,
                transform.position.y,
                transform.position.z));
    }

    public bool Init(Boid newBoid)
    {
        if (newBoid == null)
            return false;

        m_Boid = newBoid;
        transform.position =
            new Vector3(m_Boid.position.x, m_Boid.position.y, m_Boid.position.z);

        return true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position =
            new Vector3(
                m_Boid.position.x,
                m_Boid.position.y,
                m_Boid.position.z);
    }
}
