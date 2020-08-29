using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningEvents : MonoBehaviour
{
    public static WarningEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action onToolClicked;
    public void ToolClicked()
    {
        if(onToolClicked != null)
        {
            onToolClicked();
        }
    }

}
