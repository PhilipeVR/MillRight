using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePiece : MonoBehaviour
{
    [SerializeField] private Vector3 initialFinalPosition, initialStockPosition;
    [SerializeField] private Transform referenceTransformX, referenceTransformY;
    [SerializeField] private Animator animator;
    [SerializeField] private PlacePieceTrigger trigger;
    [SerializeField] private float time1, time2, time3, time4, Speed, tableXref, tableYref;
    private AnimationClip clip;
    private Vector3 initialReferencePositionX, initialReferencePositionY, offset, constantReference;
    private Boolean clicked;
    private void Awake()
    {
        clip = animator.runtimeAnimatorController.animationClips[0];
        initialReferencePositionX.x = tableXref;
        initialReferencePositionY.y = tableYref;
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
            //Debug.Log("Offset X: " + offset.x.ToString());
            //Debug.Log("Offset Y: " + offset.y.ToString());
            //Debug.Log("LocalPos X: " + transform.localPosition.x.ToString());
            //Debug.Log("LocalPos Y: " + transform.localPosition.y.ToString());
            //Debug.Log("LocalPos Z: " + transform.localPosition.z.ToString());

            Vector3 offsetVal = new Vector3(transform.localPosition.x + offset.x, transform.localPosition.y + offset.y, transform.localPosition.z + offset.z);
            //Debug.Log(offsetVal);
            //Debug.Log("Val X: " + offsetVal.x.ToString());
            //Debug.Log("Val Y: " + offsetVal.y.ToString());
            //Debug.Log("Val Z: " + offsetVal.z.ToString());
            //Debug.Log(transform.localPosition);

            gameObject.transform.localPosition = offsetVal;
        }
    }

    public void ResetAnim()
    {
        animator.enabled = true;
        animator.speed = 0;
        gameObject.transform.localPosition = initialStockPosition;
        offset = new Vector3(0, 0, 0);
        constantReference.x = referenceTransformX.localPosition.x;
        constantReference.y = referenceTransformY.localPosition.y;
        initialReferencePositionX.x = tableXref;
        initialReferencePositionY.y = tableYref;
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
