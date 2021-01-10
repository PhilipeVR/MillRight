﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTriggers : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private TriggerAnimationController[] triggerController;
    [SerializeField] private ProcessAnimationController manager;
    [SerializeField] private DRO_Button dRO_Button;
    [SerializeField] private bool LockButton;
    private List<Trigger> triggers;

    // Start is called before the first frame update
    void Awake() {
        triggers = animTriggers.triggers;
        ResetTriggers(-1);
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        TriggerAnimationController controller = triggerController[manager.Index];
        foreach (Trigger trigger in triggers)
        {

            if(trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
            {
                tmpTrigger = trigger;
                break;
            }
        }
        if(tmpTrigger != null)
        {
            Boolean val = tmpTrigger.PlaySequence(controller);
            if(val)
            {
                if(dialogueManager.SentenceIndex == tmpTrigger.CurrentSentenceIndex())
                {
                    dialogueManager.DisplayNextSentence();
                    if (LockButton)
                    {
                        dRO_Button.Btn_LockSetEnabled(true);
                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        PlaySequence();
    }

    public void ResetTriggers(int anim)
    {
        foreach (Trigger trigger in triggers)
        {
            if (anim < 0)
            {
                trigger.Index = 0;
            }
            else if (trigger.Anim == anim)
            {
                trigger.Index = 0;
            }
            
        }
    }


}
