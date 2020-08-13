using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField] XWheelControl xWheelControl;
    [SerializeField] YWheelControl yWheelControl;
    [SerializeField] ZWheelControl zWheelControl;

    [SerializeField] DRO_Button XLockButton;
    [SerializeField] DRO_Button YLockButton;
    [SerializeField] DRO_Button ZLockButton;

    //TEMPORARY
    [SerializeField] QuillFeedControl QuillFeedControl; // TEMPORARY
    [SerializeField] DRO_Button QuillLockButton; // TEMPORARY
    [SerializeField] DRO_Button FineLockButton; // TEMPORARY


    // Update is called once per frame
    void Update()
    {
        if(XLockButton.Activated == true)
        {
            xWheelControl.enabled = true;
            yWheelControl.enabled = false;
            zWheelControl.enabled = false;
            QuillFeedControl.enabled = false; // Temporary

        }
        if(YLockButton.Activated == true)
        {
            yWheelControl.enabled = true;
            xWheelControl.enabled = false;
            zWheelControl.enabled = false;
            QuillFeedControl.enabled = false; // Temporary

        }
        if(ZLockButton.Activated == true)
        {
            zWheelControl.enabled = true;
            xWheelControl.enabled = false;
            yWheelControl.enabled = false;
            QuillFeedControl.enabled = false; // Temporary

        }
        if(QuillLockButton.Activated == true)
        {
            QuillFeedControl.enabled = true; // Temporary
            zWheelControl.enabled = false;
            xWheelControl.enabled = false;
            yWheelControl.enabled = false;

        }
    }
}