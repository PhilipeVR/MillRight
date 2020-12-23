using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProcessChecker : MonoBehaviour
{
    [SerializeField] private GameObject checkHolder, doneBTN;
    [SerializeField] private Button operationBTN;
    [SerializeField] private OperationStatus drilling, sideMilling, faceMilling;
    [SerializeField] private Toggle_On_Off powerButton;
    private OperationStatus current;
    private string currentName = "Current";

    private void Awake()
    {
        drilling.Done = false;
        sideMilling.Done = false;
        faceMilling.Done = false;
        current.OperationName = currentName;
    }

    public void ChangeListener(string x_name)
    {
        if(x_name == drilling.OperationName)
        {
            current = drilling;
        }
        else if(x_name == sideMilling.OperationName)
        {
            current = sideMilling;
        }
        else if (x_name == faceMilling.OperationName)
        {
            current = faceMilling;
        }
    }

    public void SubmitCheckmark()
    {
        if(current.OperationName != currentName)
        {
            if (!powerButton.isON)
            {
                if (current.EnoughCubeDestroyed)
                {
                    current.Done = true;
                    current.Checkmark.SetActive(true);
                    TriggerCongrats();
                    checkHolder.SetActive(false);
                    doneBTN.SetActive(false);
                    operationBTN.interactable = true;
                }
            }
            else
            {
                WarningEvents.current.TurnOFFMill();
                checkHolder.SetActive(false);
                doneBTN.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void TriggerCongrats() {
        
        if (current.Done && current.OperationName == drilling.OperationName)
        {
            drilling.Done = true;
            if (drilling.Done && faceMilling.Done && sideMilling.Done)
            {
                WarningEvents.current.AllCompleted();
            }
            else
            {
                WarningEvents.current.DrillingCompleted();
            }
        }
        else if (current.Done && current.OperationName == sideMilling.OperationName)
        {
            sideMilling.Done = true;
            if (drilling.Done && faceMilling.Done && sideMilling.Done)
            {
                WarningEvents.current.AllCompleted();
            }
            else
            {
                WarningEvents.current.SideMillingCompleted();
            }
        }
        else if (current.Done && current.OperationName == faceMilling.OperationName)
        {
            faceMilling.Done = true;
            if (drilling.Done && faceMilling.Done && sideMilling.Done)
            {
                WarningEvents.current.AllCompleted();
            } 
            else
            {
                WarningEvents.current.FaceMillingCompleted();
            }
        }
    }

    [Serializable]
    public struct OperationStatus
    {
        [SerializeField] private string nameOfOperation;
        [SerializeField] private int numOfCubesToDestroy;
        [SerializeField] private RevertDestruction revertDestruction;
        [SerializeField] private GameObject checkmark;
        private Boolean done;

        public string OperationName
        {
            get => nameOfOperation;
            set => nameOfOperation = value;
        }

        public Boolean EnoughCubeDestroyed
        {
            get => revertDestruction.CubeDestroyed > numOfCubesToDestroy;
        }

        public Boolean Done
        {
            get => done;
            set => done = value;
        }

        public GameObject Checkmark
        {
            get => checkmark;
        }

    }


}
