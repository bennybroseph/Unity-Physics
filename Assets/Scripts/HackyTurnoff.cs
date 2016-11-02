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
        foreach (var clothTriangle in Resources.FindObjectsOfTypeAll<SpringDamperBehaviour>())
        {
            clothTriangle.gameObject.SetActive(!clothTriangle.gameObject.activeSelf);
        }
    }
    public void OnToggleParticles()
    {
        foreach (var clothTriangle in Resources.FindObjectsOfTypeAll<ParticleBehaviour>())
        {
            clothTriangle.gameObject.SetActive(!clothTriangle.gameObject.activeSelf);
        }
    }
}
