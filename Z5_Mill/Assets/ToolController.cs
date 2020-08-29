using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private DialogueTrigger warningTrigger;
    [SerializeField] private int warningIndex;
    [SerializeField] private int sentenceIndex;
    [SerializeField] private SwitchBit bitState;
    [SerializeField] private OperationSelection operation;
    [SerializeField] private string OperationName;
    [SerializeField] private Boolean type;
    void Start()
    {
        WarningEvents.current.onToolClicked += OnToolClicked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnToolClicked()
    {
        if (type)
        {
            //warningTrigger.TriggerSentenceDialogue(warningIndex, sentenceIndex);
        }
        else
        {

        }
        //Actions
    }
}
