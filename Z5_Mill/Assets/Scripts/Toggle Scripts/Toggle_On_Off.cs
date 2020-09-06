using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle_On_Off : MonoBehaviour
{
    [SerializeField] private OperationSelection selection;
    [SerializeField] private SwitchBit bitState;
    [SerializeField] private ZWheelControl zWheelControl;
    [SerializeField] private string NoNameOperation;
    private PlacePiece placePiece;
    private ClampPiece clampPiece;
    Boolean millOn = false;
    public String OnText, OffText;
    public GameObject manager;
    private Boolean state;

    public void Awake()
    {
        millOn = true;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
        GetComponent<Image>().color = Color.green;
        state = false;
        clampPiece = null;
        placePiece = null;

    }

    public void OnOffToggle()
    {
        GameButtonManager buttonManager = manager.GetComponent<GameButtonManager>();

        if (millOn)
        {
            Boolean testClamp = clampPiece != null;
            Boolean testPlace = placePiece != null;
            if (selection.Current.Name.Equals(NoNameOperation))
            {
                Debug.Log("LOCATED");
                WarningEvents.current.OperationSelected();
                millOn = !millOn;

            }
            else if (!bitState.CheckState())
            {
                WarningEvents.current.ToolSelection();
                millOn = !millOn;

            }
            else if(testPlace && placePiece.animTime < 1f)
            {
                WarningEvents.current.PlacePiece();
                millOn = !millOn;
            }
            else if (testClamp && clampPiece.animTime < 1f)
            {
                WarningEvents.current.ClampWorkpiece();
                millOn = !millOn;
            }
            else if (!zWheelControl.Locked && ((selection.Current.Name == selection.FaceMill.Name) || (selection.Current.Name == selection.SideMill.Name)))
            {
                WarningEvents.current.LockZ();
                millOn = !millOn;
            }
            else
            {
                transform.GetChild(0).gameObject.GetComponent<Text>().text = OffText;
                GetComponent<Image>().color = Color.red;
                if (buttonManager != null)
                {
                    buttonManager.turnOn();
                }
                state = true;
            }


        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
            GetComponent<Image>().color = Color.green;
            if (buttonManager != null)
            {
                buttonManager.turnOff();
            }
            state = false;


        }
        millOn = !millOn;
    }

    public void EmergencyStop()
    {
        GameButtonManager buttonManager = manager.GetComponent<GameButtonManager>();
 
        transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
        GetComponent<Image>().color = Color.green;
        if (buttonManager != null)
        {
            buttonManager.turnOff();
        }
        state = false;
        millOn = true;
    }

    public Boolean getMillState()
    {
        return millOn;
    }

    public Boolean isON
    {
        get => state;
    }

    public ClampPiece Clamp {
        get => clampPiece;
        set => clampPiece = value;
    }

    public PlacePiece Place
    {
        get => placePiece;
        set => placePiece = value;
    }

    public Boolean PowerState
    {
        get => state;
    }
}
