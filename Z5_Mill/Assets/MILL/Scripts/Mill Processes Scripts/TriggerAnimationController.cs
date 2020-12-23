﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggerAnimationController : MonoBehaviour
{
    int index = 0;
    private Animator animator;
    [SerializeField] private HintTriggerFlash triggerFlash;
    [SerializeField] private string[] clipName;
    [SerializeField] private string resetParam, restartParam;
    private List<AnimatorControllerParameter> parameters;
    private Boolean transitionDone, animDone, activated;

    private void Awake()
    {
        parameters = new List<AnimatorControllerParameter>();
        animator = GetComponent<Animator>();
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if(param.type == AnimatorControllerParameterType.Bool)
            {
                parameters.Add(param);
            }
        }
    
        
    }

    public void activate(Boolean state)
    {
        activated = state;
    }

    public void StartAnimation()
    {
        
        animator.SetBool(parameters[0].name, true);
    }


    public Boolean PlayAnimation(string transition, string animationName)
    {

        if (index == 0 && activated && transition == parameters[index].name)
        {
            ResetLoopParams();
            StartAnimation();
            index++;
            triggerFlash.StopRoutine();
            triggerFlash.AnimPlayed();
            return true;
        }


        if (index < parameters.Count && activated) {
            Boolean inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
            if (transition == parameters[index].name && inRightTransition && animationName == clipName[index])
            {
                animator.SetBool(transition, true);
            
                index++;
                checkTransitionEnd();
                triggerFlash.StopRoutine();
                triggerFlash.AnimPlayed();
                return true;
            }
        }
        return false;
    }

    public void checkTransitionEnd()
    {
        if(index == parameters.Count && activated)
        {
            transitionDone = true;
        }
    }

    public void setAnimState(Boolean state)
    {
        animDone = state;
        transitionDone = state;
    }

    public void ResetParams()
    {
        for (int i = 0; i < parameters.Count; i++)
        {
            animator.SetBool(parameters[i].name, false);
        }
        index = 0;

    }

    private void ResetLoopParams()
    {
        animator.SetFloat(resetParam, 0f);
        animator.SetFloat(restartParam, 0f);
    }

    public void ResetAnim()
    {
        animator.SetFloat(resetParam, 1f);

    }

    public void RestartAnim()
    {
        animator.SetFloat(restartParam, 1f);

    }

    public Boolean Active
    {
        get => activated;
    }

    public int Index
    {
        get => index;
    }

    public Boolean Done
    {
        get => animDone;
    }

    public string ResetParam
    {
        get => resetParam;
    }

    public string RestartParam
    {
        get => restartParam;
    }

    public Boolean CurrentAnimationStatus
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }
}
