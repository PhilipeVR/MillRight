using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private int animIndex;
    [SerializeField] private string OnAnimationName, OffAnimationName, drillTag, stopTag, initialClip;
    [SerializeField] private GameObject cutter, dummyCutter, holder;
    [SerializeField] private HintTrigger triggerFlash;
    [SerializeField] private string[] clipName;
    [SerializeField] private string resetParam, restartParam;
    [SerializeField] private List<ButtonTrigger> buttonTriggers;
    [SerializeField] private List<RendererTrigger> rendererTriggers;
    private List<AnimatorControllerParameter> parameters;
    private bool transitionDone, animDone;
    private int m_counter, m_index = 0;
    private bool isActive = false;
    private AnimationEvent eventSys1, eventSys2;

    void Awake()
    {
        eventSys1 = new AnimationEvent();
        eventSys1.functionName = "TurnOn";
        eventSys2 = new AnimationEvent();
        eventSys2.functionName = "TurnOFF";
        parameters = new List<AnimatorControllerParameter>();
        
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.type == AnimatorControllerParameterType.Bool)
            {
                parameters.Add(param);
            }
        }

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

    public void ResetAnim(bool prevState, OperationManager prevOperationManager)
    {
        SetAnimState(false);
        ResetParams();

        if (prevState)
        {
            prevOperationManager.ResetAnim();
        }
        if (m_counter > 0)
        {
            foreach (ButtonTrigger clipTriggers in buttonTriggers)
            {
                clipTriggers.ResetTriggers(animIndex);
            }            
            foreach (RendererTrigger clipTriggers in rendererTriggers)
            {
                if (!clipTriggers.gameObject.activeSelf)
                {
                    clipTriggers.gameObject.SetActive(true);
                }
                clipTriggers.ResetTriggers(animIndex);
            }
            RestartAnim();
        }
        ActivateAnimator(true);
        m_counter++;
    }

    public void ActivateAnimator(bool state)
    {
        animator.Rebind();
        animator.enabled = state;
        holder.SetActive(state);
        cutter.SetActive(state);
        dummyCutter.SetActive(state);
        isActive = state;
    }

    public void StartAnimation()
    {

        animator.SetBool(parameters[0].name, true);
    }

    public bool PlayAnimation(string transition, string animationName)
    {


        if (m_index == 0 && isActive && transition == parameters[m_index].name)
        {
            ResetLoopParams();
            StartAnimation();
            m_index++;
            triggerFlash.StopRoutine();
            triggerFlash.AnimPlayed();
            return true;
        }


        if (m_index < parameters.Count && isActive)
        {


            bool inRightTransition = animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
            if (transition == parameters[m_index].name && inRightTransition && animationName == clipName[m_index])
            {
                animator.SetBool(transition, true);

                m_index++;
                CheckTransitionEnd();
                triggerFlash.StopRoutine();
                triggerFlash.AnimPlayed();
                return true;
            }
        }
        return false;
    }

    public void ResetParams()
    {
        for (int i = 0; i < parameters.Count; i++)
        {
            animator.SetBool(parameters[i].name, false);
        }
        m_index = 0;

    }

    public void CheckTransitionEnd()
    {
        if (m_index == parameters.Count && isActive)
        {
            transitionDone = true;
        }
    }

    public void TurnOn()
    {
        dummyCutter.tag = drillTag;
        animator.SetFloat("TurnOFF", 1);
    }

    public void TurnOFF()
    {
        dummyCutter.tag = stopTag;
        animator.SetFloat("TurnOFF", 0);
        SetAnimState(true);
        isActive = false;
    }

    public void SetAnimSpeed(float speed)
    {
        animator.SetFloat("Speed", speed);
    }

    public void SetAnimState(bool state)
    {
        animDone = state;
        transitionDone = state;
    }

    public Animator Animation
    {
        get => animator;
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

    public int Index
    {
        get => m_index;
    }

    public bool Active
    {
        get => isActive;
    }

    public bool Done
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

    public bool CurrentAnimationStatus
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f;
    }


}
