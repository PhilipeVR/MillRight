using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_On_Off : MonoBehaviour
{
    Boolean millOn = false;
    public String OnText, OffText;
    public GameObject manager;

    public void Awake()
    {
        millOn = false;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = OffText;
        GetComponent<Image>().color = Color.red;

    }

    public void OnOffToggle()
    {
        GameButtonManager buttonManager = manager.GetComponent<GameButtonManager>();

        if (millOn)
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
            GetComponent<Image>().color = Color.green;
            if (buttonManager != null)
            {
                buttonManager.turnOn();
            }

        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = OffText;
            GetComponent<Image>().color = Color.red;
            if(buttonManager != null)
            {
                buttonManager.turnOff();
            }


        }
        millOn = !millOn;
    }

    public Boolean getMillState()
    {
        return millOn;
    }
}
