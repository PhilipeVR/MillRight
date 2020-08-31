using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Policy;
using UnityEngine;

public class WarningSubscribers : MonoBehaviour
{
    [SerializeField] WarningTrigger warningTrigger;
    [SerializeField] List<WarningTriggerInformation> information;
    void Start()
    {
        WarningEvents.current.cutterNear += CutterNearWarning;
        WarningEvents.current.clampWorkpiece += ClampPieceBefore;
        WarningEvents.current.operationSelected += OperationNotSelected;
        WarningEvents.current.toolSelection += ToolsNotSelected;
        WarningEvents.current.placePiece += PlacePieceBefore;
        WarningEvents.current.stopTableMovement += StopTableMovement;
        WarningEvents.current.cantChangeCutter += CantChangeCutter;
        WarningEvents.current.turnOFF += NotMillingPiece;
        WarningEvents.current.noAction += NoAnimAction;
    }

    // Update is called once per frame
    private void CutterNearWarning()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("CutterNearWarning");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void ClampPieceBefore()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("ClampPieceBefore");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void OperationNotSelected()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("OperationNotSelected");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void ToolsNotSelected()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("ToolsNotSelected");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void StopTableMovement()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("StopTableMovement");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void PlacePieceBefore()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("PlacePieceBefore");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void CantChangeCutter()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("CantChangeCutter");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void NotMillingPiece()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("NotMillingPiece");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void NoAnimAction()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("NoAnimAction");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }


    private WarningTriggerInformation FindWarningInfo(string eventName)
    {
        WarningTriggerInformation res = new WarningTriggerInformation();
        foreach(WarningTriggerInformation triggerInformation in information)
        {
            if (eventName.Equals(triggerInformation.EventName))
            {
                res = triggerInformation;
                break;
            }
        }
        return res;
    }

    [Serializable]
    private struct WarningTriggerInformation
    {
        [SerializeField] private string eventName;
        [SerializeField] private int dialogIndex;
        [SerializeField] private int sentenceIndex;

        public string EventName
        {
            get => eventName;
        }

        public int DialogIndex
        {
            get => dialogIndex;
        }

        public int SentenceIndex
        {
            get => sentenceIndex;
        }

    } 
}
