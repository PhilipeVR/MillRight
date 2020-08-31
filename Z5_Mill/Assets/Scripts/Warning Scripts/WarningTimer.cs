using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTimer : MonoBehaviour
{
    [SerializeField] Toggle_On_Off PowerButton;
    [SerializeField] XWheelControl xWheelControl;
    [SerializeField] YWheelControl yWheelControl;
    [SerializeField] ZWheelControl zWheelControl;
    [SerializeField] QuillFeedControl QuillFeedControl; 
    [SerializeField] FineAdjustmentControl FineButton;
    [SerializeField] float CountdownSeconds;

    private float xTime, yTime, zTime, quillTime, fineTime;
    public float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = CountdownSeconds;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PowerButton.PowerState)
        {
            if(CheckAnimTime())
            {
                timer -= Time.deltaTime;
                if(timer < 0)
                {
                    WarningEvents.current.NoAction();
                    PowerButton.EmergencyStop();
                }
            }
            else
            {
                GetAnimatorsTime();
            }
        }
        else
        {
            timer = CountdownSeconds;
        }
    }

    void GetAnimatorsTime()
    {
        xTime = xWheelControl.animTime;
        yTime = yWheelControl.animTime;
        zTime = zWheelControl.animTime;
        quillTime = QuillFeedControl.animTime;
        fineTime = FineButton.animTime;
        timer = CountdownSeconds;
    }

    private Boolean CheckAnimTime()
    {
        Boolean xState = xTime.Equals(xWheelControl.animTime);
        Boolean yState = yTime.Equals(yWheelControl.animTime);
        Boolean zState = zTime.Equals(zWheelControl.animTime);
        Boolean quillState = quillTime.Equals(QuillFeedControl.animTime);
        Boolean fineState = fineTime.Equals(FineButton.animTime);
        return xState && yState && zState && quillState && fineState;
    }
}
