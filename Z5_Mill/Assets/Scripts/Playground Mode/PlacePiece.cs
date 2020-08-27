using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePiece : MonoBehaviour
{
    [SerializeField] private Vector3 initialFinalPosition;
    [SerializeField] private Transform referenceTransformX, referenceTransformY;
    [SerializeField] private Animator animator;
    [SerializeField] private float time1, time2, time3, time4, Speed;
    private AnimationClip clip;
    private Vector3 initialStockPosition, initialReferencePositionX, initialReferencePositionY, offset, constantReference;
    private void Awake()
    {
        clip = animator.runtimeAnimatorController.animationClips[0];
        animator.speed = 0;
        offset = new Vector3(0, 0, 0);
        initialStockPosition = transform.localPosition;
        constantReference.x = referenceTransformX.localPosition.x;
        constantReference.y = referenceTransformY.localPosition.y;
        //Debug.Log("Pos: " + transform.position.ToString());
        //Debug.Log("Local Pos: " + transform.localPosition.ToString());
        initialReferencePositionX = referenceTransformX.localPosition;
        initialReferencePositionY = referenceTransformY.localPosition;
    }

    // Update is called once per frame
    public void PlaceStock()
    {
        float offSetVal;
        AnimationCurve curve;
        offset.x = referenceTransformX.localPosition.x - initialReferencePositionX.x;
        offset.y = referenceTransformY.localPosition.y - initialReferencePositionY.y;
        constantReference.x = referenceTransformX.localPosition.x;
        constantReference.y = referenceTransformY.localPosition.y;
        //Debug.Log("referenceTransformY: " + referenceTransformY.localPosition.y.ToString());
        //Debug.Log("initialReferencePositionY: " + initialReferencePositionY.y.ToString());
        Keyframe[] keysX = new Keyframe[4];
        offSetVal = initialFinalPosition.x + offset.x;


        keysX[0] = new Keyframe(time1, initialStockPosition.x);
        keysX[1] = new Keyframe(time2, initialStockPosition.x);
        keysX[2] = new Keyframe(time3, initialStockPosition.x);
        keysX[3] = new Keyframe(time4, offSetVal);

        curve = new AnimationCurve(keysX);

        clip.SetCurve("", typeof(Transform), "localPosition.x", curve);

        Keyframe[] keysY = new Keyframe[4];
        offSetVal = initialFinalPosition.y + offset.y;

        keysY[0] = new Keyframe(time1, initialStockPosition.y);
        keysY[1] = new Keyframe(time2, initialStockPosition.y);
        keysY[2] = new Keyframe(time3, offSetVal);
        keysY[3] = new Keyframe(time4, offSetVal);

        curve = new AnimationCurve(keysY);

        clip.SetCurve("", typeof(Transform), "localPosition.y", curve);

        Keyframe[] keysZ = new Keyframe[4];
        offSetVal = initialFinalPosition.z + offset.z;

        keysZ[0] = new Keyframe(time1, initialStockPosition.z);
        keysZ[1] = new Keyframe(time2, offSetVal);
        keysZ[2] = new Keyframe(time3, offSetVal);
        keysZ[3] = new Keyframe(time4, offSetVal);

        curve = new AnimationCurve(keysZ);

        clip.SetCurve("", typeof(Transform), "localPosition.z", curve);

        animator.Play(clip.name,0,0);
        animator.speed = Speed;

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
            Debug.Log("LocalPos X: " + transform.localPosition.x.ToString());
            Debug.Log("LocalPos Y: " + transform.localPosition.y.ToString());
            Debug.Log("LocalPos Z: " + transform.localPosition.z.ToString());

            Vector3 offsetVal = new Vector3(transform.localPosition.x + offset.x, transform.localPosition.y + offset.y, transform.localPosition.z + offset.z);
            Debug.Log(offsetVal);
            Debug.Log("Val X: " + offsetVal.x.ToString());
            Debug.Log("Val Y: " + offsetVal.y.ToString());
            Debug.Log("Val Z: " + offsetVal.z.ToString());
            //Debug.Log(transform.localPosition);

            gameObject.transform.localPosition = offsetVal;
        }
    }

    public void ResetAnim()
    {
        animator.enabled = true;
        animator.speed = 0;
        gameObject.transform.localPosition = initialStockPosition;
    }
}
