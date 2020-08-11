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
    //[SerializeField] private DRO_DisplayHandler displayHandler;
    [SerializeField] private DRO_Button quillFeed; 
    [SerializeField] private DRO_Button fineAdjust; 

    bool toggle; // Activate or deactivate buttons

    void start()
    {

    }


    // These functions could alsmost certainly be replaced by using dictionaries
    // Also, get rid of serialized references by using find object of type or something similar
    // Clean-up later, time permitting

    public void Click_X(){

        toggle = x.Activated;
        xLock.Btn_SetEnabled(!toggle);
        x.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            yLock.Btn_SetEnabled(toggle);
            zLock.Btn_SetEnabled(toggle);
            
            y.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Y(){

        toggle = y.Activated;
        yLock.Btn_SetEnabled(!toggle);
        y.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            xLock.Btn_SetEnabled(toggle);
            zLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
            fineAdjust.Btn_SetEnabled(toggle);
        }
    }

    public void Click_Z(){

        toggle = z.Activated;
        zLock.Btn_SetEnabled(!toggle);
        z.Btn_SetEnabled(!toggle);

        if (toggle == false) // Ensures all other buttons are deactivated
        {
            xLock.Btn_SetEnabled(toggle);
            yLock.Btn_SetEnabled(toggle);
            
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
            xLock.Btn_SetEnabled(toggle);
            yLock.Btn_SetEnabled(toggle);
            zLock.Btn_SetEnabled(toggle);
            
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
            xLock.Btn_SetEnabled(toggle);
            yLock.Btn_SetEnabled(toggle);
            zLock.Btn_SetEnabled(toggle);
            
            x.Btn_SetEnabled(toggle);
            y.Btn_SetEnabled(toggle);
            z.Btn_SetEnabled(toggle);

            quillFeed.Btn_SetEnabled(toggle);
        }
    }

}
