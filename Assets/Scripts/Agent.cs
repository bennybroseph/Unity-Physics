using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class Agent : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> m_Targets = new List<GameObject>();

    [SerializeField]
    private Vector3 m_Velocity;
    [SerializeField]
    private Vector3 m_Steering;

    [SerializeField]
    [Range(-5f, 5f)]
    private float m_SteeringPriority = 1f;

    [SerializeField]
    private float m_Mass = 1f;

    public List<GameObject> targets
    {
        get { return m_Targets; }
        set { m_Targets = value; }
    }

    public float mass
    {
        get { return m_Mass; }
        set { m_Mass = value; }
    }

    public float steeringPriority
    {
        get { return m_SteeringPriority; }
        set { m_SteeringPriority = value; }
    }

    // Use this for initialization
    void Start()
    {
        var test = FindObjectsOfType<Target>().Select(x => x.gameObject);

        m_Targets.AddRange(test);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var target in m_Targets)
        {
            m_SteeringPriority =
            Mathf.Clamp(
                50f - Vector3.Distance(target.transform.position, transform.position), 0f, 50f);

            m_Steering = (target.transform.position - transform.position) * m_SteeringPriority;
            m_Steering = m_Steering.normalized;

            m_Velocity += m_Steering / m_Mass;
        }

        transform.position += m_Velocity * Time.deltaTime;
    }
}
