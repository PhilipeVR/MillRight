using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    [SerializeField] private WarningTrigger warningTrigger;
    [SerializeField] private int warningIndex;
    [SerializeField] private int sentenceIndex;
    [SerializeField] private SwitchBit bitState;
    [SerializeField] private OperationSelection operation;
    [SerializeField] private string OperationName;
    [SerializeField] private Boolean type, holder;

    private string sideMilling = "Side Milling";
    private string drilling = "Drilling";
    private string faceMilling = "Face Milling";

    public void OnToolClicked()
    {
        GameObject toolHolder;
        if (holder)
        {
            toolHolder = bitState.DrillChuck;
        }
        else
        {
            toolHolder = bitState.EndMillHolder;
        }
        if (type)
        {
            if(!toolHolder.activeSelf && bitState.holderState)
            {
                if(holder)
                {
                    WarningEvents.current.WrongHolderEndMillHolder();
                    bitState.Reset();
                }
                else
                {
                    WarningEvents.current.WrongHolderDrillChuck();
                    bitState.Reset();
                }
            }
            else if (OperationName == operation.Current.Name && bitState.holderState)
            {
                if(OperationName == sideMilling)
                {
                    WarningEvents.current.SideMillingCutter();
                    bitState.Reset();
                }
                else if (OperationName == faceMilling)
                {
                    WarningEvents.current.FaceMillingCutter();
                    bitState.Reset();
                }
                else if (OperationName == drilling)
                {
                    WarningEvents.current.DrillingCutter();
                    bitState.Reset();
                }
            }
        }
    }


}
