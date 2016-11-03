using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
    [SerializeField]
    private Particle m_Particle;

    private bool m_MoveWithMouse;
    private bool m_WasKinematic;

    private Renderer m_Renderer;

    public Vector3 m_PreviousMousePosition = Vector3.zero;

    private readonly Color m_SelectedColor = new Color32(255, 87, 34, 255);
    private readonly Color m_PinnedColor = new Color32(3, 169, 244, 255);
    private readonly Color m_DefaultColor = new Color32(255, 255, 255, 255);

    public Particle particle
    {
        get { return m_Particle; }
        set { m_Particle = value; }
    }

    public bool wasKinematic
    {
        get { return m_WasKinematic; }
        set { m_WasKinematic = value; }
    }
    public bool moveWithMouse
    {
        get { return m_MoveWithMouse; }
        set { m_MoveWithMouse = value; }
    }

    // Use this for initialization
    private void Start()
    {
        m_Renderer = GetComponent<Renderer>();

        transform.position = m_Particle.position;
    }

    // Update is called once per frame
    private void OnRenderObject()
    {
        if (m_Renderer)
        {
            m_Renderer.material.color =
                m_MoveWithMouse
                    ? m_SelectedColor
                    : m_Particle.isKinematic ? m_PinnedColor : m_DefaultColor;
        }

        transform.position = m_Particle.position;
    }

    private void Update()
    {
        if (!m_MoveWithMouse)
        {
            m_PreviousMousePosition = Vector3.zero;
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            m_Particle.isKinematic = m_WasKinematic;
            m_PreviousMousePosition = Vector3.zero;

            m_MoveWithMouse = false;
        }

        var mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;

        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (m_PreviousMousePosition == Vector3.zero)
            m_PreviousMousePosition = mousePosition;

        m_Particle.position = (Vector3)m_Particle.position + (mousePosition - m_PreviousMousePosition);
        m_PreviousMousePosition = mousePosition;

        if (Input.GetKeyDown(KeyCode.P))
            m_WasKinematic = !m_WasKinematic;
    }

    public static ParticleBehaviour Create(GameObject baseGameObject = null)
    {
        // If a baseGameObject was passed in instantiate it, otherwise create a default
        var newGameObject =
            baseGameObject ?
            Instantiate(baseGameObject) : new GameObject();

        newGameObject.name = "New Particle";

        var newParticle = new Particle();
        var newParticleBehaviour = newGameObject.AddComponent<ParticleBehaviour>();

        newParticleBehaviour.particle = newParticle;

        return newParticleBehaviour;
    }
}
