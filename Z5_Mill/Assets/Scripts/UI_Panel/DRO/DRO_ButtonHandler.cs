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

    public string currentUnits;
    
    private void Start()
    {
        // Must follow same order in hierarchy tree, depth-first search
        //inchButton = GetComponentInChildren<DRO_ButtonState>();
        //mmButton = GetComponentInChildren<DRO_ButtonState>();
    }
 
    public void ToggleOtherButton(DRO_ButtonState buttonState)
    {
        // Debug.Log("inch: "+(buttonState.buttonName == "inchButton"));
        // Debug.Log("mm: "+(buttonState.buttonName == "mmButton"));
        // Debug.Log("\n");

        if(buttonState.buttonName == "inchButton")
        {          
            mmButton.ToggleThisButton();
        }
        else if(buttonState.buttonName == "mmButton")
        {
            inchButton.ToggleThisButton();
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
                zLockButton.DisableThisButton(); //Enable LockButton means unlocking that button
            }
            
            xLockButton.DisableThisButton();
            yLockButton.DisableThisButton();
        }
        else {} // Do nothing

    }



}
