using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TriggerAnimationController : MonoBehaviour
{
    int index = 0;
    private Animator animator;
    [SerializeField] private string[] clipName;
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
        /*string s = "| ";
        foreach (AnimationClip clip in clips)
        {
            s += clip.name + " | ";
        }
        string d = "| ";
        foreach (AnimatorControllerParameter cf in parameters)
        {
            d += cf.name + " | ";
        }
        Debug.Log(s);
        Debug.Log(d);
        */
    }

    public void activate(Boolean state)
    {
        activated = state;
    }

    public void StartAnimation()
    {
        
        animator.SetBool(parameters[0].name, true);
    }


    public void PlayAnimation(string transition, string animationName)
    {


        if (index == 0 && activated && transition == parameters[index].name)
        {
            StartAnimation();
            index++;
            return;
        }

        if (index < parameters.Count - 1 && activated) {
            Debug.Log(animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Boolean inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f;
            Debug.Log(inRightTransition);
            if (transition == parameters[index].name && inRightTransition && animationName == clipName[index])
            {
                animator.SetBool(transition, true);
                index++;
                checkTransitionEnd();
            }
        }
        return;

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
}
