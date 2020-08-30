﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningEvents : MonoBehaviour
{
    public static WarningEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action cutterChangeON;
    public void CutterChangeON()
    {
        if(cutterChangeON != null)
        {
            cutterChangeON();
        }
    }

    public event Action cutterNear;
    public void CutterNear()
    {
        if (cutterNear != null)
        {
            cutterNear();
        }
    }

    public event Action turnOFF;
    public void TurnOFF()
    {
        if (turnOFF != null)
        {
            turnOFF();
        }
    }

    public event Action viseTableCollison;
    public void ViseTableCollison()
    {
        if (viseTableCollison != null)
        {
            viseTableCollison();
        }
    }

    public event Action clampWorkpiece;

    public void ClampWorkpiece()
    {
        if(clampWorkpiece != null)
        {
            clampWorkpiece();
        }
    }

    public event Action placePiece;

    public void PlacePiece()
    {
        if (placePiece != null)
        {
            placePiece();
        }
    }

    public event Action toolSelection;

    public void ToolSelection()
    {
        if (toolSelection != null)
        {
            toolSelection();
        }
    }

    public event Action operationSelected;

    public void OperationSelected()
    {
        if (operationSelected != null)
        {
            operationSelected();
        }
    }



}
