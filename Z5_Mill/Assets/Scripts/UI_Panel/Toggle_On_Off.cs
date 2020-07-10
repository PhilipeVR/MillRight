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
        if (!millOn)
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
            GetComponent<Image>().color = Color.green;
            manager.GetComponent<GameButtonManager>().turnOn();

        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = OffText;
            GetComponent<Image>().color = Color.red;
            manager.GetComponent<GameButtonManager>().turnOff();


        }
        millOn = !millOn;
    }
}
