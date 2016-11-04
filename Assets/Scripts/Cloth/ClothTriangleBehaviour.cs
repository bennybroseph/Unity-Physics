using System.Linq;

using UnityEngine;

namespace Cloth
{
    public class ClothTriangleBehaviour : MonoBehaviour
    {
        [SerializeField]
        private ClothTriangle m_ClothTriangle;

        private static Material lineMaterial;

        private static Color32 s_FrontColor = new Color32(33, 150, 243, 255);
        private static Color32 s_BackColor = new Color32(244, 67, 54, 255);

        public ClothTriangle clothTriangle
        {
            get { return m_ClothTriangle; }
            set { m_ClothTriangle = value; }
        }

        private void OnRenderObject()
        {
            if (m_ClothTriangle.isTorn)
                return;

            m_ClothTriangle.CalculateNormals();

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            var light = FindObjectsOfType<Light>().First(x => x.type == LightType.Directional);
            var lightDirection = -light.transform.forward;

            GL.Begin(GL.TRIANGLES);
            {
                var diffuseTerm = Vector3.Dot(lightDirection, m_ClothTriangle.normal);
                diffuseTerm = Mathf.Max(0f, diffuseTerm);

                Color32 diffuse = s_FrontColor * light.color * diffuseTerm;
                diffuse.a = 255;

                GL.Color(diffuse);

                GL.Vertex3(
                    m_ClothTriangle.particle1.position.x,
                    m_ClothTriangle.particle1.position.y,
                    m_ClothTriangle.particle1.position.z);
                GL.Vertex3(
                    m_ClothTriangle.particle2.position.x,
                    m_ClothTriangle.particle2.position.y,
                    m_ClothTriangle.particle2.position.z);
                GL.Vertex3(
                    m_ClothTriangle.particle3.position.x,
                    m_ClothTriangle.particle3.position.y,
                    m_ClothTriangle.particle3.position.z);

                diffuseTerm = Vector3.Dot(lightDirection, -m_ClothTriangle.normal);
                diffuseTerm = Mathf.Max(0f, diffuseTerm);

                diffuse = s_FrontColor * light.color * diffuseTerm;
                diffuse.a = 255;

                GL.Color(diffuse);

                GL.Vertex3(
                    m_ClothTriangle.particle3.position.x,
                    m_ClothTriangle.particle3.position.y,
                    m_ClothTriangle.particle3.position.z);
                GL.Vertex3(
                    m_ClothTriangle.particle2.position.x,
                    m_ClothTriangle.particle2.position.y,
                    m_ClothTriangle.particle2.position.z);
                GL.Vertex3(
                    m_ClothTriangle.particle1.position.x,
                    m_ClothTriangle.particle1.position.y,
                    m_ClothTriangle.particle1.position.z);
            }
            GL.End();
        }

        public static ClothTriangleBehaviour Create(
            Particle particle1, Particle particle2, Particle particle3)
        {
            var newGameObject = new GameObject { name = "New Cloth Triangle" };

            var newClothTriangle = new ClothTriangle
            {
                particle1 = particle1,
                particle2 = particle2,
                particle3 = particle3
            };
            var newClothTriangleBehaviour = newGameObject.AddComponent<ClothTriangleBehaviour>();

            newClothTriangleBehaviour.m_ClothTriangle = newClothTriangle;

            return newClothTriangleBehaviour;
        }

        private static void CreateLineMaterial()
        {
            if (lineMaterial)
                return;

            // Unity has a built-in shader that is useful for drawing
            // simple colored things.
            var shader = Shader.Find("Hidden/Internal-Colored");
            lineMaterial = new Material(shader) { hideFlags = HideFlags.HideAndDontSave };

            // Turn on alpha blending
            //lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            //lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Back);
            // Turn off depth writes
            //lineMaterial.SetInt("_ZWrite", 0);
        }
    }
}
