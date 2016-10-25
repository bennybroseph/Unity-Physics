using System;

using UnityEngine;
using System.Globalization;
using System.Reflection;

using UnityEngine.UI;

using Object = UnityEngine.Object;

public class UISliderHook : MonoBehaviour
{
    [SerializeField]
    private Slider m_Slider;
    [SerializeField]
    private Text m_Text;

    [SerializeField]
    private Object m_Target;
    [SerializeField]
    private string m_TargetType;
    [SerializeField]
    private string m_TargetProperty;

    // Use this for initialization
    void Start()
    {
        name = m_TargetProperty + " " + name;

        if (!m_Slider)
            m_Slider = GetComponentInChildren<Slider>();

        if (m_Slider)
        {
            var type = m_Target ? m_Target.GetType() : Type.GetType(m_TargetType);
            if (type != null)
            {
                var method = type.GetProperty(m_TargetProperty);
                m_Slider.value = (float)method.GetValue(m_Target, null);

                m_Slider.onValueChanged.AddListener(
                    delegate (float newValue)
                    {
                        method = type.GetProperty(m_TargetProperty);
                        method.SetValue(m_Target, newValue, null);
                    });
            }
            else
            {
                if (m_Target)
                    Debug.Log("The requested target \"" + m_Target + "\" does not exist");
                else
                    Debug.Log("The requested type \"" + m_TargetType + "\" does not exist");
            }
        }

        if (!m_Text)
            m_Text = GetComponentInChildren<Text>();

        if (m_Text)
            m_Text.text = m_TargetProperty;
    }
}
