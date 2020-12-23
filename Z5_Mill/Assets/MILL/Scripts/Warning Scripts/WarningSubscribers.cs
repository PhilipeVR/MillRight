﻿using System;
using System.Collections;
using System.Collections.Generic;
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
        WarningEvents.current.sideMillingCutter += SideMillingCutter;
        WarningEvents.current.faceMillingCutter += FaceMillingCutter;
        WarningEvents.current.drillingCutter += DrillingCutter;
        WarningEvents.current.wrongHolderDrillChuck += WrongHolderDrill;
        WarningEvents.current.wrongHolderEndMillHolder += WrongHolderEndMill;
        WarningEvents.current.faceMillingCompleted += FaceMillingDone;
        WarningEvents.current.sideMillingCompleted += SideMillingDone;
        WarningEvents.current.drillingCompleted += DrillingDone;
        WarningEvents.current.allCompleted += AllCompleted;
        WarningEvents.current.turnOFFMill += TurnOFFMill;
        WarningEvents.current.lockX += LockXAxis;
        WarningEvents.current.lockZ += LockZAxis;
        WarningEvents.current.zeroX += ZeroXAxis;
        WarningEvents.current.zeroZ += ZeroZAxis;
        WarningEvents.current.pieceFirst += PieceSetup;
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

    private void PieceSetup()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("PieceSetup");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void SideMillingCutter()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("SideMillingCutter");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void FaceMillingCutter()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("FaceMillingCutter");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }


    private void DrillingCutter()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("DrillingCutter");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void WrongHolderDrill()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("WrongHolderDrill");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void WrongHolderEndMill()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("WrongHolderEndMill");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }


    private void SideMillingDone()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("SideMillingDone");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }


    private void FaceMillingDone()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("FaceMillingDone");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }


    private void DrillingDone()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("DrillingDone");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void AllCompleted()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("AllCompleted");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void TurnOFFMill()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("TurnOFFMill");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void LockXAxis()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("LockXAxis");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void LockZAxis()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("LockZAxis");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void ZeroXAxis()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("ZeroXAxis");
        warningTrigger.TriggerSentence(triggerInformation.DialogIndex, triggerInformation.SentenceIndex);
    }

    private void ZeroZAxis()
    {
        WarningTriggerInformation triggerInformation = FindWarningInfo("ZeroZAxis");
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
