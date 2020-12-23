using System;
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
    public event Action cantChangeCutter;
    public void CantChangeCutter()
    {
        if(cantChangeCutter != null)
        {
            cantChangeCutter();
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

    public event Action stopTableMovement;
    public void StopTableMovement()
    {
        if (stopTableMovement != null)
        {
            stopTableMovement();
        }
    }

    public event Action noAction;
    public void NoAction()
    {
        if (noAction != null)
        {
            noAction();
        }
    }

    public event Action pieceFirst;
    public void PieceFirst()
    {
        if (pieceFirst != null)
        {
            pieceFirst();
        }
    }

    public event Action sideMillingCutter;
    public void SideMillingCutter()
    {
        if (sideMillingCutter != null)
        {
            sideMillingCutter();
        }
    }

    public event Action faceMillingCutter;
    public void FaceMillingCutter()
    {
        if (faceMillingCutter != null)
        {
            faceMillingCutter();
        }
    }

    public event Action drillingCutter;
    public void DrillingCutter()
    {
        if (drillingCutter != null)
        {
            drillingCutter();
        }
    }

    public event Action wrongHolderDrillChuck;
    public void WrongHolderDrillChuck()
    {
        if (wrongHolderDrillChuck != null)
        {
            wrongHolderDrillChuck();
        }
    }

    public event Action wrongHolderEndMillHolder;
    public void WrongHolderEndMillHolder()
    {
        if (wrongHolderEndMillHolder != null)
        {
            wrongHolderEndMillHolder();
        }
    }

    public event Action drillingCompleted;
    public void DrillingCompleted()
    {
        if (drillingCompleted != null)
        {
            drillingCompleted();
        }
    }

    public event Action faceMillingCompleted;
    public void FaceMillingCompleted()
    {
        if (faceMillingCompleted != null)
        {
            faceMillingCompleted();
        }
    }


    public event Action sideMillingCompleted;
    public void SideMillingCompleted()
    {
        if (sideMillingCompleted != null)
        {
            sideMillingCompleted();
        }
    }

    public event Action allCompleted;
    public void AllCompleted()
    {
        if (allCompleted != null)
        {
            allCompleted();
        }
    }

    public event Action turnOFFMill;
    public void TurnOFFMill()
    {
        if (turnOFFMill != null)
        {
            turnOFFMill();
        }
    }

    public event Action lockZ;
    public void LockZ()
    {
        if (lockZ != null)
        {
            lockZ();
        }
    }

    public event Action lockX;
    public void LockX()
    {
        if (lockX != null)
        {
            lockX();
        }
    }

    public event Action zeroZ;
    public void ZeroZ()
    {
        if (zeroZ != null)
        {
            zeroZ();
        }
    }

    public event Action zeroX;
    public void ZeroX()
    {
        if (zeroX != null)
        {
            zeroX();
        }
    }



}
