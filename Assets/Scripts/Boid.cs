using System;
using System.Collections.Generic;

#if UNITY_5
using UnityEngine;
#endif

using Utility;

[Serializable]
public class Boid
{
#if UNITY_5
    [SerializeField]
#endif
    private Vector m_Position;
#if UNITY_5
    [SerializeField]
#endif
    private Vector m_Velocity;

    private static readonly List<Boid> s_Boids = new List<Boid>();

    private static float s_Cohesion = 100f;
    private static float s_Separation = 10f;
    private static float s_Alignment = 10f;

    private static float s_VelocityLimit = 40f;

    private static float s_TendTowardsPower = 5f;
    private static Vector s_TendToPosition;

    public static float cohesion
    {
        get { return s_Cohesion; }
        set { s_Cohesion = value; }
    }
    public static float separation
    {
        get { return s_Separation; }
        set { s_Separation = value; }
    }
    public static float alignment
    {
        get { return s_Alignment; }
        set { s_Alignment = value; }
    }

    public static float velocityLimit
    {
        get { return s_VelocityLimit; }
        set { s_VelocityLimit = value; }
    }

    public static float tendTowardsPower
    {
        get { return s_TendTowardsPower; }
        set { s_TendTowardsPower = value; }
    }
    public static Vector tendToPosition
    {
        get { return s_TendToPosition; }
        set { s_TendToPosition = value; }
    }

    public Vector position
    {
        get { return m_Position; }
    }

    public Vector velocity
    {
        get { return m_Velocity; }
    }

    public Boid(Vector position = new Vector())
    {
        m_Position = position;

        s_Boids.Add(this);
    }

    public static bool Update(float deltaTime)
    {
        if (s_Boids.Count == 0)
            return false;

        foreach (var boid in s_Boids)
        {
            var cohesion = boid.Cohesion();
            var separation = boid.Separation();
            var alignment = boid.Alignment();

            var tendTowards = boid.TendTowards();

            boid.m_Velocity += cohesion + alignment + separation + tendTowards;
            if (boid.m_Velocity.Magnitude() > s_VelocityLimit)
                boid.m_Velocity = boid.m_Velocity / boid.m_Velocity.Magnitude() * s_VelocityLimit;

            boid.m_Position += boid.m_Velocity * deltaTime;
        }

        return true;
    }

    // Cohesion
    private Vector Cohesion()
    {
        var percievedCenter = new Vector();
        foreach (var boid in s_Boids)
            if (boid != this)
                percievedCenter += boid.m_Position;

        percievedCenter /= s_Boids.Count - 1;

        return (percievedCenter - m_Position) / s_Cohesion;
    }

    // Separation
    private Vector Separation()
    {
        var displacement = new Vector();
        foreach (var boid in s_Boids)
            if (boid != this)
                if ((boid.m_Position - m_Position).Magnitude() <= s_Separation)
                    displacement -= boid.m_Position - m_Position;

        return displacement;
    }

    // Alignment
    private Vector Alignment()
    {
        var percievedVelocity = new Vector();
        foreach (var boid in s_Boids)
        {
            if (boid != this)
                percievedVelocity += boid.m_Velocity;
        }

        percievedVelocity /= s_Boids.Count - 1;

        return (percievedVelocity - m_Velocity) / s_Alignment;
    }

    private Vector TendTowards()
    {
        return (s_TendToPosition - position) / s_TendTowardsPower;
    }

    ~Boid()
    {
        s_Boids.Remove(this);
    }
}
