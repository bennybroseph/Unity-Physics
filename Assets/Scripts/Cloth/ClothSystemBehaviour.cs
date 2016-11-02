using Cloth;

using UnityEngine;
public class ClothSystemBehaviour : MonoBehaviour
{
    [SerializeField]
    private ClothSystem m_ClothSystem;

    [SerializeField]
    private bool m_isPaused;

    public ClothSystem clothSystem
    {
        get { return m_ClothSystem; }
        set { m_ClothSystem = value; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            m_isPaused = !m_isPaused;

        if (!m_isPaused)
            m_ClothSystem.Update(Time.deltaTime);
    }

    public static ClothSystemBehaviour Create()
    {
        var newGameObject = new GameObject { name = "New Cloth System" };

        var newClothSystem = new ClothSystem();
        var newClothSystemBehaviour = newGameObject.AddComponent<ClothSystemBehaviour>();

        newClothSystemBehaviour.clothSystem = newClothSystem;

        return newClothSystemBehaviour;
    }
}
