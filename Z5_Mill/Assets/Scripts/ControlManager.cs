using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] XWheelControl xWheelControl;
    [SerializeField] YWheelControl yWheelControl;

    [SerializeField] DRO_ButtonState XLockButton;
    [SerializeField] DRO_ButtonState YLockButton;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(XLockButton.checkIfEnabled == true)
        {
            xWheelControl.enabled = true;
            yWheelControl.enabled = false;

        }
        if(YLockButton.checkIfEnabled == true)
        {
            yWheelControl.enabled = true;
            xWheelControl.enabled = false;

        }
    }
}