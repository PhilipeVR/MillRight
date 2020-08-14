using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private string OnAnimationName;
    [SerializeField] private string OffAnimationName;
    [SerializeField] private GameObject bit, holder, dummyBit, vise;
    [SerializeField] private string drillTag;
    [SerializeField] private string stopTag;

    private AnimationClip initialClip;
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

        initialClip = animator.runtimeAnimatorController.animationClips[0];

    }



    public void ResetAnim()
    {
        animator.Play(initialClip.name, -1, 0);
        isActive = true;
        ActivateAnimator(true);
    }
    public void ActivateAnimator(Boolean state)
    {
        bit.SetActive(state);
        animator.enabled = state;
        holder.SetActive(state);
        vise.SetActive(state);
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

        isActive = false;
    }

    public void setAnimSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public Boolean inAction()
    {
        return isActive;
    }
}
