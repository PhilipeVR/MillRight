using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_Button : MonoBehaviour
{
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] private string buttonName; 

    private bool m_activated;
    private Image m_buttonImage;

  
    void Awake()
    {
        m_buttonImage = GetComponent<Image>();
        m_buttonImage.color = disabledColor;
        m_activated = false;
    }

    public void Btn_SetEnabled(bool value)
    {
        if (value == true)
        {
            m_buttonImage.color = enabledColor;
            m_activated = true;
        }
        else
        {
            m_buttonImage.color = disabledColor;
            m_activated = false;
        }

    }

    public void Btn_LockSetEnabled(bool value)
    {
        if (value == true)
        {
            m_buttonImage.color = disabledColor;
            m_activated = true;
        }
        else
        {
            m_buttonImage.color = enabledColor;
            m_activated = false;
        }

    }

    public void Btn_Toggle()
    {
        if (m_activated)
        {
            m_buttonImage.color = enabledColor;
        }
        else
        {
            m_buttonImage.color = disabledColor;
        }

        m_activated = !m_activated;

    }

    public bool Activated
    {
        get => m_activated;
        set => m_activated = value;
    }

    public Image ButtonImage
    {
        get => m_buttonImage;
        set => m_buttonImage = value;
    }
    

}
