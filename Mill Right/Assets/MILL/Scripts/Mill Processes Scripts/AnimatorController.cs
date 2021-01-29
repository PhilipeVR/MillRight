using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private int animIndex;
    [SerializeField] private string OnAnimationName;
    [SerializeField] private string OffAnimationName;
    [SerializeField] private GameObject bit, holder, dummyBit, vise;
    [SerializeField] private AnimClipTrigger placePieceTrigger;
    [SerializeField] private PowerTriggers powerTriggers;
    [SerializeField] private List<ClipTriggers> buttonTriggers;
    [SerializeField] private string drillTag;
    [SerializeField] private string stopTag;
    [SerializeField] private string initialClip;

    private int counter = 0;
    private Boolean isActive = true;
    private AnimationEvent eventSys1, eventSys2;
    private Animator animator;
    void Awake()
    {
        eventSys1 = new AnimationEvent();
        eventSys1.functionName = "TurnOn";
        eventSys2 = new AnimationEvent();
        eventSys2.functionName = "TurnOFF";

        animator = GetComponent<Animator>();

        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == OnAnimationName)
            {
                clip.AddEvent(eventSys1);
            }
            else if (clip.name == OffAnimationName)
            {
                clip.AddEvent(eventSys2);
            }
        }

    }

    public void ResetAnim(Boolean prevState, AnimatorController prevAnim)
    {
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();
        if (triggerAnimation != null)
        {
            triggerAnimation.setAnimState(false);
            triggerAnimation.ResetParams();
        }
        RevertDestruction revert = vise.GetComponentInChildren<RevertDestruction>();
        if (revert != null)
        {
            revert.RevertStock();
        }
        if (prevState)
        {
            prevAnim.setReset();
        }
        if (counter > 0)
        {
            foreach(ClipTriggers clipTriggers in buttonTriggers)
            {
                clipTriggers.ResetTriggers(animIndex);
            }
            placePieceTrigger.Reset();
            powerTriggers.ResetTriggers(animIndex) ;
            setRestart();
        }


        isActive = true;
        ActivateAnimator(true);
        counter++;
    }
    public void ActivateAnimator(Boolean state)
    {
        if (state)
        {
            animator.Rebind();
        }
        bit.SetActive(state);
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.enabled = state;
        holder.SetActive(state);
        vise.SetActive(state);
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();
        if (triggerAnimation != null)
        {
            triggerAnimation.activate(state);
        }
    }

    public void TurnOn()
    {
        dummyBit.tag = drillTag;
        animator.SetFloat("TurnOFF", 1);
    }

    public void TurnOFF()
    {
        dummyBit.tag = stopTag;
        animator.SetFloat("TurnOFF", 0);
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();
        if(triggerAnimation != null)
        {
            triggerAnimation.setAnimState(true);
        }
        isActive = false;
    }

    public void setAnimSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public Boolean Active
    {
        get => isActive;
    }

    public Boolean AnimationDone()
    {
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();

        if (triggerAnimation != null)
        {
            return triggerAnimation.Done;
        }

        return false;
    }

    public void setRestart()
    {
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();

        if (triggerAnimation != null)
        {
            triggerAnimation.RestartAnim();
        }
    }

    public void setReset()
    {
        TriggerAnimationController triggerAnimation = GetComponent<TriggerAnimationController>();

        if (triggerAnimation != null)
        {
            triggerAnimation.ResetAnim();
        }
    }

    public TriggerAnimationController TriggerAnimation
    {
        get => GetComponent<TriggerAnimationController>();
    }
}
