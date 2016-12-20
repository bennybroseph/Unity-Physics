using UnityEngine;
using System.Collections;

using Cloth;

public class HackyTurnoff : MonoBehaviour
{
    private void Start()
    {
        foreach (var clothTriangle in Resources.FindObjectsOfTypeAll<ClothTriangleBehaviour>())
            clothTriangle.gameObject.SetActive(false);
    }

    public void OnToggleTriangles()
    {
        foreach (var clothTriangle in Resources.FindObjectsOfTypeAll<ClothTriangleBehaviour>())
        {
            clothTriangle.gameObject.SetActive(!clothTriangle.gameObject.activeSelf);
        }
    }
    public void OnToggleSprings()
    {
        foreach (var springDamperBehaviour in Resources.FindObjectsOfTypeAll<SpringDamperBehaviour>())
        {
            springDamperBehaviour.gameObject.SetActive(!springDamperBehaviour.gameObject.activeSelf);
        }
    }
    public void OnToggleParticles()
    {
        foreach (var particleBehaviour in Resources.FindObjectsOfTypeAll<ParticleBehaviour>())
        {
            var particleRenderer = particleBehaviour.GetComponent<Renderer>();
            if (particleRenderer)
                particleRenderer.enabled = !particleRenderer.enabled;
        }
    }
    public void OnToggleWind()
    {
        ClothTriangle.s_Wind = !ClothTriangle.s_Wind;
    }
}
