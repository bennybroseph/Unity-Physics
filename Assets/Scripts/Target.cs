using UnityEngine;
using System.Collections;

public class Target : MonoBehaviour
{
    [SerializeField]
    private int m_Agents = 5;

    // Use this for initialization
    void Awake()
    {
        Random.InitState(System.DateTime.Now.Millisecond);

        var parent = new GameObject(name + "'s Initial Targets");
        for (var i = 0; i < m_Agents; ++i)
        {
            var newGameObject = new GameObject("Agent " + i);
            newGameObject.transform.SetParent(parent.transform, false);

            var newAgent = newGameObject.AddComponent<Agent>();
            newAgent.mass = 0.5f + Random.value;

            var newSphere = newGameObject.AddComponent<Sphere>();
            newSphere.radius = newAgent.mass / 2f;
            newSphere.GenSphere();

            newGameObject.transform.position =
                new Vector3(
                    Random.value * 10f * newAgent.mass,
                    Random.value * 10f * newAgent.mass,
                    Random.value * 10f * newAgent.mass);

            newGameObject.transform.position = newGameObject.transform.position + transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
