using UnityEngine;



public class BoidBehaviourSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Prefab;

    [SerializeField]
    private int m_NumberOfBoids;

    [SerializeField]
    private Vector3 m_MinPosition;
    [SerializeField]
    private Vector3 m_MaxPosition;

    // Use this for initialization
    private void Awake()
    {
        for (var i = 0; i < m_NumberOfBoids; ++i)
        {
            var newGameObject = Instantiate(m_Prefab);
            newGameObject.name = "Boid" + i;

            newGameObject.transform.SetParent(transform, false);

            var newBoid = BoidFactory.Create(m_MinPosition, m_MaxPosition);

            var newBoidBehaviour = newGameObject.AddComponent<BoidBehaviour>();
            newBoidBehaviour.Init(newBoid);
        }
        Boid.avoidPosition = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        Boid.Update(Time.deltaTime);

        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (Input.GetMouseButton(1))
            Boid.tendToPosition = new Vector3(mousePosition.x, mousePosition.y);

        if (Input.GetMouseButton(2))
            Boid.avoidPosition = new Vector3(mousePosition.x, mousePosition.y);

        transform.position = Boid.avoidPosition;
    }
}
