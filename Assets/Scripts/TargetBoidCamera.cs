using UnityEngine;
using System.Collections;

public class TargetBoidCamera : MonoBehaviour {

    private Vector3 m_Offset;

    void Awake()
    {
        m_Offset = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var averagePosition = new Vector3();

        var agents = FindObjectsOfType<BoidBehaviour>();
        foreach (var agent in agents)
            averagePosition += agent.transform.position;

        transform.LookAt(averagePosition / agents.Length);

        //averagePosition = Vector3.zero;

        //var targets = FindObjectsOfType<BoidBehaviour>();
        //foreach (var target in targets)
        //    averagePosition += target.transform.position;

        //transform.position = averagePosition / targets.Length + m_Offset;
    }
}
