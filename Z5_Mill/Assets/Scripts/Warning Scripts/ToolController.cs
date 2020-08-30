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
    [SerializeField] private Boolean type;


    public void OnToolClicked()
    {
        if (type)
        {
            if (OperationName == operation.Current.Name && bitState.holderState)
            {
                warningTrigger.TriggerSentence(warningIndex, sentenceIndex);
            }
        }
        else
        {

        }
    }
}
