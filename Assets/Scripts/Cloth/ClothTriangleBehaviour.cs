using UnityEngine;

namespace Cloth
{
    public class ClothTriangleBehaviour : MonoBehaviour
    {
        [SerializeField]
        private ClothTriangle m_ClothTriangle;

        public static Material lineMaterial;

        public ClothTriangle clothTriangle
        {
            get { return m_ClothTriangle; }
            set { m_ClothTriangle = value; }
        }

        private void OnRenderObject()
        {
            if (m_ClothTriangle.isTorn)
                return;

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            GL.Begin(GL.TRIANGLES);
            {
                GL.Color(
                    new Color(
                        m_ClothTriangle.particle1.position.normalized.x,
                        m_ClothTriangle.particle1.position.normalized.y,
                        m_ClothTriangle.particle1.position.normalized.z));
                GL.Vertex3(
                    m_ClothTriangle.particle1.position.x,
                    m_ClothTriangle.particle1.position.y,
                    m_ClothTriangle.particle1.position.z);

                GL.Color(
                    new Color(
                        m_ClothTriangle.particle2.position.normalized.x,
                        m_ClothTriangle.particle2.position.normalized.y,
                        m_ClothTriangle.particle2.position.normalized.z));
                GL.Vertex3(
                    m_ClothTriangle.particle2.position.x,
                    m_ClothTriangle.particle2.position.y,
                    m_ClothTriangle.particle2.position.z);

                GL.Color(
                    new Color(
                        m_ClothTriangle.particle3.position.normalized.x,
                        m_ClothTriangle.particle3.position.normalized.y,
                        m_ClothTriangle.particle3.position.normalized.z));
                GL.Vertex3(
                    m_ClothTriangle.particle3.position.x,
                    m_ClothTriangle.particle3.position.y,
                    m_ClothTriangle.particle3.position.z);
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
            lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            // Turn backface culling off
            lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
            // Turn off depth writes
            //lineMaterial.SetInt("_ZWrite", 0);
        }
    }
}
