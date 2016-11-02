using UnityEngine;

namespace Cloth
{
    public class SpringDamperBehaviour : MonoBehaviour
    {
        [SerializeField]
        private SpringDamper m_SpringDamper;

        static Material lineMaterial;

        public SpringDamper springDamper
        {
            get { return m_SpringDamper; }
            set { m_SpringDamper = value; }
        }

        private void OnRenderObject()
        {
            if (m_SpringDamper.isTorn)
                return;

            CreateLineMaterial();
            lineMaterial.SetPass(0);

            var test =
                Vector3.Lerp(
                    new Vector3(0f, 1f, 0f),
                    new Vector3(1f, 0f, 0f),
                    (m_SpringDamper.tail.position - m_SpringDamper.head.position).magnitude) /
                    (m_SpringDamper.restLength * m_SpringDamper.tearLength);

            GL.Begin(GL.LINES);
            {
                GL.Color(
                    new Color(test.x, test.y, test.z));
                GL.Vertex3(
                    m_SpringDamper.head.position.x,
                    m_SpringDamper.head.position.y,
                    m_SpringDamper.head.position.z);
                GL.Vertex3(
                    m_SpringDamper.tail.position.x,
                    m_SpringDamper.tail.position.y,
                    m_SpringDamper.tail.position.z);
            }
            GL.End();
        }

        public static SpringDamperBehaviour Create(Particle head, Particle tail)
        {
            var newGameObject = new GameObject { name = "New Spring Damper" };

            var newSpringDamper = new SpringDamper(head, tail);
            var newSpringDamperBehaviour = newGameObject.AddComponent<SpringDamperBehaviour>();

            newSpringDamperBehaviour.springDamper = newSpringDamper;

            return newSpringDamperBehaviour;
        }

        static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
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
}
