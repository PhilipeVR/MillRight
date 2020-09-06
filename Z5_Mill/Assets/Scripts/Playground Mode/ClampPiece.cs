using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPiece : MonoBehaviour
{
    [SerializeField] private ZWheelControl WheelControl;
    [SerializeField] private QuillFeedControl QuillControl;
    [SerializeField] private FineAdjustmentControl fineAdjustmentControl;
    [SerializeField] private SwitchBit checkBitState;
    [SerializeField] private Animator animator, prevAnimator;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private Color basic;
    [SerializeField] private float Speed;
    private Color basicColor;
    private Boolean Clicked;

    void Start()
    {
        GetComponent<Renderer>().material.color = basicColor;
        animator.speed = 0;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = basicColor;
    }

    private void OnMouseOver()
    {
        if (prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !Clicked)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(WheelControl.animTime);
        Debug.Log("Quill Height: " + QuillControl.Height);
        Debug.Log("Quill Max Height: " + QuillControl.MaxHeight);
        Debug.Log("Fine Height: " + QuillControl.MaxHeight);
        Debug.Log("Fine Max Height: " + QuillControl.MaxHeight);

        if ((WheelControl.animTime > 0 || (QuillControl.Height < QuillControl.MaxHeight)) && checkBitState.CheckState())
        {
            WarningEvents.current.CutterNear();
        }
        else if (prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !Clicked)
        {
            GetComponent<Renderer>().material.color = clickedColor;
            animator.speed = Speed;
            Clicked = true;
        }
    }

    public float animTime
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime * animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public void Reset()
    {
        GetComponent<Renderer>().material.color = basicColor;
        animator.Play(animator.runtimeAnimatorController.animationClips[0].name, 0, 0);
        animator.speed = 0;
        Clicked = false;
    }
}
