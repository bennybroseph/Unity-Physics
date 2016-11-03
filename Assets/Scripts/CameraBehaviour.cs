using UnityEngine;



public class CameraBehaviour : MonoBehaviour
{
    private Vector3 m_PreviousMousePosition;
    private Vector3 m_PreviousCameraRotation;

    // Use this for initialization
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            m_PreviousMousePosition = Input.mousePosition;
            m_PreviousCameraRotation = Camera.main.transform.eulerAngles;
        }

        if (Input.GetMouseButton(1))
        {
            var mousePosition = Input.mousePosition;
            var deltaMousePosition = (mousePosition - m_PreviousMousePosition) / 10f;

            Camera.main.transform.eulerAngles +=
                new Vector3(-deltaMousePosition.y, deltaMousePosition.x);

            m_PreviousMousePosition = mousePosition;
        }

        if (Input.GetMouseButtonDown(2))
        {
            m_PreviousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(2))
        {
            var mousePosition = Input.mousePosition;
            var deltaMousePosition = (mousePosition - m_PreviousMousePosition) / 100f;

            Camera.main.transform.position +=
                deltaMousePosition.x * -Camera.main.transform.right;
            Camera.main.transform.position +=
                deltaMousePosition.y * -Camera.main.transform.up;

            m_PreviousMousePosition = mousePosition;
        }

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            Camera.main.transform.position += 0.4f * Input.mouseScrollDelta.y * Camera.main.transform.forward;
        }
    }
}
