using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPiece : MonoBehaviour
{
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
        Debug.Log(prevAnimator.name);
        if (prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !Clicked)
        {
            GetComponent<Renderer>().material.color = clickedColor;
            animator.speed = Speed;
            Clicked = true;
        }
    }

    public void Reset()
    {
        GetComponent<Renderer>().material.color = basicColor;
        animator.Play(animator.runtimeAnimatorController.animationClips[0].name, 0, 0);
        animator.speed = 0;
        Clicked = false;
    }
}
