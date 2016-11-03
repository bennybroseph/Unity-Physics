using UnityEngine;
using System.Collections;

using Cloth;

public class HackyTurnoff : MonoBehaviour
{
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
            particleBehaviour.gameObject.SetActive(!particleBehaviour.gameObject.activeSelf);
        }
    }
}
