using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToggle : MonoBehaviour
{
    public GameObject toggleObject;
    private Boolean isActive = true;
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
}
