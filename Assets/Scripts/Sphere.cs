using System;

using UnityEngine;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class Sphere : MonoBehaviour
{
    [Serializable]
    private class Vertex
    {
        public Vector3 position;
        public Vector4 colour;
    }

    private MeshRenderer m_MeshRenderer;
    private MeshFilter m_MeshFilter;

    [SerializeField]
    private float m_Radius = 1f;
    [SerializeField]
    private float m_Segments = 25f;
    [SerializeField]
    private float m_Points = 25f;

    [SerializeField]
    private List<Vertex> m_VertexCache;

    public float radius
    {
        get { return m_Radius; }
        set { m_Radius = value; }
    }

    private void OnValidate()
    {
        GenSphere();
    }

    // Use this for initialization
    public void GenSphere()
    {
        m_VertexCache = GenVertexes(m_Radius, m_Segments, m_Points).ToList();

        var meshRenderer = gameObject.GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Standard"));
        meshRenderer.sharedMaterial.SetFloat("_Glossiness", 1f);

        var meshFilter = gameObject.GetComponent<MeshFilter>();

        var mesh = new Mesh { name = "Custom Sphere" };
        meshFilter.mesh = mesh;

        var positions = m_VertexCache.Select(vertex => vertex.position).ToArray();
        var normals = m_VertexCache.Select(vertex => vertex.position.normalized).ToArray();
        var colours = m_VertexCache.Select(vertex => vertex.colour);

        mesh.vertices = positions;
        mesh.normals = normals;

        mesh.triangles = GenIndexes(m_Segments, m_Points).ToArray();
    }

    private static IEnumerable<Vertex> GenVertexes(float radius, float segments, float points)
    {
        var halfCircle = GenHalfCircle(radius, points).ToList();
        for (var i = 0f; i < segments; ++i)
        {
            var phi = 2 * Mathf.PI * (i / segments);
            foreach (var vertex in halfCircle)
            {
                yield return new Vertex
                {
                    position =
                        new Vector3(
                            vertex.position.x * Mathf.Cos(phi) + vertex.position.z * -Mathf.Sin(phi),
                            vertex.position.y,
                            vertex.position.x * Mathf.Sin(phi) + vertex.position.z * Mathf.Cos(phi)),
                    colour = vertex.colour,
                };
            }
        }
    }

    private static IEnumerable<Vertex> GenHalfCircle(float radius, float points)
    {
        for (var i = 0f; i < points; ++i)
        {
            var theta = Mathf.PI * i / (points - 1f);

            yield return new Vertex
            {
                position = new Vector3(Mathf.Sin(theta) * radius, Mathf.Cos(theta) * radius, 0f),
                colour = new Vector4(1f, 1f, 1f, 1f),
            };
        }
    }

    private static IEnumerable<int> GenIndexes(float segments, float points)
    {
        //j=np-1
        //
        //2     5   8   11  14  17
        //1     4   7   10  13  16
        //0     3   6   9   12  15

        for (var i = 0; i < segments; ++i) //nm = 4
        {
            var start = i * points;
            for (var j = 0; j < points - 1; ++j) //np = 3
            {
                if (i >= segments - 1)
                {
                    yield return j;
                    yield return (int)(start + j + 1);
                    yield return (int)(start + j);

                    yield return j + 1;
                    yield return (int)(start + j + 1);
                    yield return j;
                }
                else
                {
                    yield return (int)(start + points + j + 1);
                    yield return (int)(start + j + 1);
                    yield return (int)(start + j);


                    yield return (int)(start + points + j);
                    yield return (int)(start + points + j + 1);
                    yield return (int)(start + j);
                }
            }
        } //we copied the origin whenever we rotated around nm + 1 times so we don't need to get the end again
    }
}
