using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampPiece : MonoBehaviour
{
    [SerializeField] private Animator animator, prevAnimator;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private float Speed;
    private Color basicColor;

    void Start()
    {
        basicColor = GetComponent<Renderer>().material.color;
        animator.speed = 0;
    }

    private void OnMouseExit()
    {
        if(prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            GetComponent<Renderer>().material.color = basicColor;
        }
        
    }

    private void OnMouseOver()
    {
        if (prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log(prevAnimator.name);
        if (prevAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            GetComponent<Renderer>().material.color = clickedColor;
            animator.speed = Speed;
        }
    }

    public void Reset()
    {
        GetComponent<Renderer>().material.color = basicColor;
        animator.Play(animator.runtimeAnimatorController.animationClips[0].name, 0, 0);
        animator.speed = 0;
    }
}
