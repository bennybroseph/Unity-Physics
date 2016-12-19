using UnityEngine;

public class FollowTargetBehaviour : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        transform.position = Boid.tendToPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Boid.tendToPosition;
    }
}
