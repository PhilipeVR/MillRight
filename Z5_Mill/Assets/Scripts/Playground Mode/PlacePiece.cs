using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePiece : MonoBehaviour
{
    [SerializeField] private Vector3 initialFinalPosition, initialStockPosition;
    [SerializeField] private Transform referenceTransformX, referenceTransformY;
    [SerializeField] private float referenceX, referenceY;
    [SerializeField] private Animator animator;
    [SerializeField] private PlacePieceTrigger trigger;
    [SerializeField] private float Speed;
    private AnimationClip clip;
    private Vector3 offset, constantReference;
    private Boolean clicked;
    private void Awake()
    {
        clip = animator.runtimeAnimatorController.animationClips[0];

        ResetAnim();
    }

    // Update is called once per frame
    public void PlaceStock()
    {

        animator.Play(clip.name,0,0);
        animator.speed = Speed;
        clicked = true;


    }

    private void LateUpdate()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f)
        {
            animator.enabled = false;

            offset.x = referenceTransformX.localPosition.x - constantReference.x;
            offset.y = referenceTransformY.localPosition.y - constantReference.y;

            constantReference.x = referenceTransformX.localPosition.x;
            constantReference.y = referenceTransformY.localPosition.y;

            Vector3 offsetVal = new Vector3(transform.localPosition.x + offset.x, transform.localPosition.y + offset.y, transform.localPosition.z + offset.z);

            gameObject.transform.localPosition = offsetVal;
        }
        else
        {
            offset = new Vector3(0, 0, 0);
        }
    }

    public void ResetAnim()
    {
        animator.enabled = true;
        animator.speed = 0;
        gameObject.transform.localPosition = initialStockPosition;
        offset = new Vector3(0, 0, 0);
        constantReference.x = referenceX;
        constantReference.y = referenceY;
        clicked = false;
    }

    public void ResetTrigger()
    {
        trigger.Reset();
    }

    public float animTime
    {
        get => animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    public Boolean Clicked
    {
        get => clicked;
    }
}
