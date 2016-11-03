using System;

using Utility.Vector;

[Serializable]
public class Particle
{
#if UNITY_5
    [UnityEngine.SerializeField]
#endif
    private bool m_IsKinematic;

#if UNITY_5
    [UnityEngine.SerializeField]
#endif
    private Vector3 m_Position;

    private Vector3 m_Force;
    private Vector3 m_Acceleration;
#if UNITY_5
    [UnityEngine.SerializeField]
#endif
    private Vector3 m_Velocity;

#if UNITY_5
    [UnityEngine.Space, UnityEngine.SerializeField]
#endif
    private float m_Mass = 1f;

    public bool isKinematic
    {
        get { return m_IsKinematic; }
        set
        {
            m_IsKinematic = value;
            if (m_IsKinematic)
                m_Velocity = Vector3.zero;
        }
    }

    public Vector3 position
    {
        get { return m_Position; }
        set { m_Position = value; }
    }

    public Vector3 force
    {
        get { return m_Force; }
        set { m_Force = value; }
    }
    public Vector3 velocity
    {
        get { return m_Velocity; }
        set { m_Velocity = value; }
    }

    public float mass
    {
        get { return m_Mass; }
        set { m_Mass = value; }
    }

    public Particle() { }
    public Particle(Vector3 position, float mass)
    {
        m_Position = position;
        m_Mass = mass;
    }

    public void AddForce(Vector3 force)
    {
        if (isKinematic)
            return;

        m_Force += force;
    }

    public void Update(float deltaTime)
    {
        if (isKinematic)
            return;

        m_Acceleration = 1f / m_Mass * m_Force;

        m_Velocity += m_Acceleration * deltaTime;
        m_Position += m_Velocity * deltaTime;
    }
}
