using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DROToggle : MonoBehaviour
{
    public GameObject toggleObject;
    public Boolean isActive = false;
    public void toggle()
    {
        if (isActive)
        {
            toggleObject.SetActive(false);
        }
        else
        {
            toggleObject.SetActive(true);
        }
        isActive = !isActive;
    }

    public void activate (Boolean state)
    {
        toggleObject.SetActive(state);
        isActive = state;
    }
}
