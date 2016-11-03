using System.Collections;

using Cloth;

using UnityEngine;
using UnityEngine.UI;

public class PauseText : MonoBehaviour
{
    private Graphic m_Graphic;
    private Shadow m_Shadow;

    [SerializeField]
    private float m_FadeTime = 1f;

    // Use this for initialization
    private void Start()
    {
        m_Graphic = GetComponent<Graphic>();
        m_Shadow = GetComponent<Shadow>();

        var clothSystem = FindObjectOfType<ClothSystemBehaviour>();

        if (clothSystem)
        {
            gameObject.SetActive(clothSystem.isPaused);
            clothSystem.onPauseEvent.AddListener(OnPause);
        }
    }

    private void OnPause(bool value)
    {
        StopAllCoroutines();
        StartCoroutine(FadeGraphic(value ? 1f : 0f));
    }

    private IEnumerator FadeGraphic(float newAlpha)
    {
        if (m_Shadow && newAlpha == 0f)
        {
            var shadowColor = m_Shadow.effectColor;
            shadowColor.a = newAlpha;
            m_Shadow.effectColor = shadowColor;
        }

        if (m_Graphic)
        {
            var oldAlpha = m_Graphic.color.a;
            var graphicColor = m_Graphic.color;

            StartCoroutine(FadeShadow(newAlpha, m_FadeTime / 2f));

            var time = 0f;
            while (time <= m_FadeTime)
            {
                graphicColor = m_Graphic.color;
                graphicColor.a = Mathf.Lerp(oldAlpha, newAlpha, time / m_FadeTime);
                m_Graphic.color = graphicColor;

                time += Time.deltaTime;
                yield return false;
            }

            graphicColor.a = newAlpha;
            m_Graphic.color = graphicColor;
        }
    }

    private IEnumerator FadeShadow(float newAlpha, float waitTime = 0f)
    {
        if (!m_Shadow || newAlpha != 1f || waitTime == 0f)
            yield break;

        yield return new WaitForSeconds(waitTime);

        var oldAlpha = m_Shadow.effectColor.a;
        var shadowColor = m_Shadow.effectColor;

        var time = 0f;
        var fadeTime = m_FadeTime;
        while (time <= fadeTime)
        {
            shadowColor = m_Shadow.effectColor;
            shadowColor.a = Mathf.Lerp(oldAlpha, newAlpha, time / fadeTime);
            m_Shadow.effectColor = shadowColor;

            time += Time.deltaTime;
            yield return false;
        }

        shadowColor.a = newAlpha;
        m_Shadow.effectColor = shadowColor;
    }
}
