using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_ButtonState : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Color enabledColor;
    [SerializeField] private Color disabledColor;
    [SerializeField] public string buttonName; //change this to not hardcode

    private Button button;
    private Image image;
    
    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();

        if(button.enabled)
        {
            image.color = enabledColor;
        }
        else
        {
            image.color = disabledColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            // Toggle to its opposite setting
            if(button.enabled == true)
            {
                button.enabled = false;
                image.color = disabledColor;
            }
            else
            {
                button.enabled = true;
                image.color = enabledColor;
            }

            ToggleOtherBotton();
        }
    }

    private void ToggleOtherBotton()
    {
        FindObjectOfType<DRO_ButtonHandler>().ToggleOtherButton(this);
    }

    // This is called by DRO_ButtonHandler
    public void ToggleThisButton()
    {
        if(button.enabled == true)
        {
            button.enabled = false;
            image.color = disabledColor;
        }
        else
        {
            button.enabled = true;
            image.color = enabledColor;
        }
    }
}
