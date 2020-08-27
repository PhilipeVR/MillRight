using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public int index = 0;
    private Animator animator;
    [SerializeField] DialogueTrigger trigger;
    [SerializeField] private int dialogIndex;
    [SerializeField] private string[] clipName;
    [SerializeField] private string resetParam;
    [SerializeField] private ButtonInteractable buttonInteractable;
    [SerializeField] private Button restartButton;
    private List<AnimatorControllerParameter> parameters;
    private int counter = 0;
    private Boolean transitionDone, animDone;

    // Start is called before the first frame update
    void Awake()
    {
        restartButton.interactable = false;
        parameters = new List<AnimatorControllerParameter>();
        animator = GetComponent<Animator>();
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                parameters.Add(param);
            }
        }
    }

    // Update is called once per frame
    public Boolean PlayAnimation(string transition, string animationName)
    {
        Debug.Log("Transition: " + transition + " and Animation Name: " + animationName);
        if (index < parameters.Count)
        {
            if(index == 0)
            {
                ResetLoopParams();
            }
            Debug.Log("Animation Time: " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
            Boolean inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
            Debug.Log(inRightTransition);
            if (transition == parameters[index].name && inRightTransition && animationName == clipName[index])
            {
                animator.SetBool(transition, true);

                index++;
                CheckTransitionEnd();
                return true;
            }
        }
        return false;
    }

    public void CheckTransitionEnd()
    {
        if (index == parameters.Count)
        {
            transitionDone = true;
            restartButton.interactable = true;
        }
        counter++;
        buttonInteractable.InteractButton();
        
    }

    public void ResetParams()
    {
        Debug.Log("ResetParams: " + index);
        for (int i = 0; i < parameters.Count; i++)
        {
            animator.SetBool(parameters[i].name, false);
        }
    }

    private void ResetLoopParams()
    {
        animator.SetFloat(resetParam, 0f);
    }

    public void ResetAnim()
    {
        if (transitionDone)
        {
            Debug.Log("TriggerAnimationController - " + name + ": " + resetParam);
            animator.SetFloat(resetParam, 1f);
            ResetParams();
            index = 0;
            transitionDone = false;
            trigger.TriggerDialogue(dialogIndex);
        }

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

    public int Counter
    {
        get => counter;
    }
}
