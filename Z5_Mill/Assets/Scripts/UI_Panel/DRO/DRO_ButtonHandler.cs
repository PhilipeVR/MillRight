using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_ButtonHandler : MonoBehaviour
{
    [Space,Header("DRO Buttons")]
    [SerializeField] private DRO_ButtonState inchButton;
    [SerializeField] private DRO_ButtonState mmButton;
    [SerializeField] private DRO_ButtonState xButton;
    [SerializeField] private DRO_ButtonState yButton;
    [SerializeField] private DRO_ButtonState zButton;
    [SerializeField] private DRO_ButtonState xLockButton;
    [SerializeField] private DRO_ButtonState yLockButton;
    [SerializeField] private DRO_ButtonState zLockButton;

    //TEMPORARY BUTTONS
    [SerializeField] private DRO_ButtonState QuillLockButton; // TEMPORARY
    [SerializeField] private DRO_ButtonState FineAdjButton; // TEMPORARY


    //public string currentUnits;
    
    private void Start()
    {
        inchButton.checkIfEnabled = true;
        mmButton.checkIfEnabled = false;

    }

    public void resetDRO()
    {
        if (zButton.checkIfEnabled)
        {
            zButton.DisableThisButton();
            ToggleOtherButton(zButton);
        }
    }

    public void ToggleOtherButton(DRO_ButtonState buttonState)
    {
        // Debug.Log("inch: "+(buttonState.buttonName == "inchButton"));
        // Debug.Log("mm: "+(buttonState.buttonName == "mmButton"));
        // Debug.Log("\n");

        if(buttonState.buttonName == "inchButton")
        {          
            inchButton.EnableThisButton();
            mmButton.DisableThisButton();
        }
        else if(buttonState.buttonName == "mmButton")
        {
            mmButton.EnableThisButton();
            inchButton.DisableThisButton();
        }
        
        // XYZ Buttons
        else if(buttonState.buttonName == "xButton")
        {
            yButton.DisableThisButton();
            zButton.DisableThisButton();

            if(buttonState.checkIfEnabled == true)
            {
                xLockButton.EnableThisButton(); //Enable LockButton means unlocking that button
            }
            else
            {
                xLockButton.DisableThisButton(); //Enable LockButton means unlocking that button
            }
            
            yLockButton.DisableThisButton();
            zLockButton.DisableThisButton();
            FineAdjButton.DisableThisButton();
            QuillLockButton.DisableThisButton(); // TEMPORARY
        }

        else if(buttonState.buttonName == "yButton")
        {
            xButton.DisableThisButton();
            zButton.DisableThisButton();

            if(buttonState.checkIfEnabled == true)
            {
                yLockButton.EnableThisButton(); //Enable LockButton means unlocking that button
            } 
            else
            {
                yLockButton.DisableThisButton(); //Enable LockButton means unlocking that button
            }
            
            xLockButton.DisableThisButton();
            zLockButton.DisableThisButton();
            FineAdjButton.DisableThisButton();
            QuillLockButton.DisableThisButton(); // TEMPORARY
        }
        else if(buttonState.buttonName == "zButton")
        {
            xButton.DisableThisButton();
            yButton.DisableThisButton();

            if(buttonState.checkIfEnabled == true)
            {
                zLockButton.EnableThisButton(); //Enable LockButton means unlocking that button
            } 
            else
            {
                zLockButton.DisableThisButton();
            }
            
            xLockButton.DisableThisButton();
            yLockButton.DisableThisButton();
            FineAdjButton.DisableThisButton();
            QuillLockButton.DisableThisButton(); // TEMPORARY
        }
        //TEMPORARY
        //QUILL
        //LOCK
        //BUTTON
        else if(buttonState.buttonName == "QuillLockButton")
        {
            xButton.DisableThisButton();
            yButton.DisableThisButton();
            zButton.DisableThisButton();
            xLockButton.DisableThisButton();
            yLockButton.DisableThisButton();
            zLockButton.DisableThisButton();
            FineAdjButton.DisableThisButton();
        }

        else if (buttonState.buttonName == "FineAdjustmentButton")
        {
            xButton.DisableThisButton();
            yButton.DisableThisButton();
            zButton.DisableThisButton();


            xLockButton.DisableThisButton();
            yLockButton.DisableThisButton();
            zLockButton.DisableThisButton();
            QuillLockButton.DisableThisButton();

        }
        else {} // Do nothing

    }



}
