using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DRO_Manager : MonoBehaviour
{
    [Space,Header("DRO Buttons")]
    [SerializeField] private DRO_Button inch;
    [SerializeField] private DRO_Button mm;
    [SerializeField] private DRO_Button x;
    [SerializeField] private DRO_Button y;
    [SerializeField] private DRO_Button z;
    [SerializeField] private DRO_Button xLock;
    [SerializeField] private DRO_Button yLock;
    [SerializeField] private DRO_Button zLock;
    [SerializeField] private DRO_Button zero;
    [SerializeField] private DRO_DisplayHandler displayHandler;
    [SerializeField] private DRO_Button quillFeed; 
    [SerializeField] private DRO_Button fineAdjust; 

    bool toggle; // Activate or deactivate buttons
    private string currentAxis;

    void Start()
    {
        // Units inch default
        inch.Btn_SetEnabled(true);
        mm.Btn_SetEnabled(false);
        resetDRO();
    }

    public void resetDRO(){

        xLock.Btn_LockSetEnabled(false);
        yLock.Btn_LockSetEnabled(false);
        zLock.Btn_LockSetEnabled(false);

        x.Btn_SetEnabled(false);
        y.Btn_SetEnabled(false);
        z.Btn_SetEnabled(false);

        quillFeed.Btn_SetEnabled(false);
        fineAdjust.Btn_SetEnabled(false);

        displayHandler.ResetZero();
    }

    public void Click_X(){

        Debug.Log(x.Activated);
        toggle = x.Activated;
        //xLock.Btn_SetEnabled(!toggle);
        x.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            //yLock.Btn_SetEnabled(toggle);
            //zLock.Btn_SetEnabled(toggle);
            
            y.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Y(){

        toggle = y.Activated;
        //yLock.Btn_SetEnabled(!toggle);
        y.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            //xLock.Btn_SetEnabled(toggle);
            //zLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Z(){

        toggle = z.Activated;
        //zLock.Btn_SetEnabled(!toggle);
        z.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            //xLock.Btn_SetEnabled(toggle);
            //yLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            y.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_QuillFeed(){

        toggle = quillFeed.Activated;
        quillFeed.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            //xLock.Btn_SetEnabled(toggle);
            //yLock.Btn_SetEnabled(toggle);
            //zLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            y.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_FineAdjust(){

        toggle = fineAdjust.Activated;
        fineAdjust.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            //xLock.Btn_SetEnabled(toggle);
            //yLock.Btn_SetEnabled(toggle);
            //zLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            y.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Inch(){

        toggle = inch.Activated;
        inch.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            mm.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Mm(){

        toggle = mm.Activated;
        mm.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            inch.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Zero()
    {
        if(x.Activated)
        {
            displayHandler.zero("x");
        }
        if(y.Activated)
        {
            displayHandler.zero("y");
        }
        if(fineAdjust.Activated || quillFeed.Activated || z.Activated)
        {
            displayHandler.zero("z");
        }
    }

}
