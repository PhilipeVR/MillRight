using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZWheelControl : MonoBehaviour
{
    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] DRO_Button ZLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    [SerializeField] private float speedMultiplier;

    private PlacePiece placePiece;
    private ClampPiece clampPiece;
    private float new_time, prev_time, prev_distance;
    public float currentSpeed;

    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Awake()
    {
        prev_time = 0;
        prev_distance = 0;
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();

        setSpeed(0.2f);
        setLockSpeed(0.5f);
        pause();
        pauseLock();
        //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

        handle_enabled = true;
        wheel_spin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(ZLockButton.Activated == true)
        {
            float distance, time;
            if (Input.mouseScrollDelta.y != 0)
            {
                distance = Math.Abs(Input.mouseScrollDelta.y - prev_distance);
                time = Math.Abs(Time.time - prev_time);
                currentSpeed = (distance / time) * speedMultiplier;
            }

            if (Input.mouseScrollDelta.y > 0f)
            {
                Boolean testPlace = placePiece != null;
                Boolean testClamp = clampPiece != null;
                if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                {
                    StopMovement();
                }
                else
                {
                    object_anim.SetFloat("Reverse", 1);
                    setSpeed(currentSpeed);
                }
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                Boolean testPlace = placePiece != null;
                Boolean testClamp = clampPiece != null;
                if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f )|| (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                {
                    StopMovement();
                }
                else
                {
                    object_anim.SetFloat("Reverse", -1);
                    setSpeed(currentSpeed);
                }
            } else
            {
                pause();
            }

        }
    }

    private void StopMovement()
    {
        pause();
        WarningEvents.current.CutterNear();
    }

    private void pause()
    {
        object_anim.speed = 0;
        animated = false;
    }

    private void pauseLock()
    {
        lock_anim.speed = 0;
    }

    private void setSpeed(float mph)
    {
        //Debug.Log(mph);
        object_anim.speed = mph;
        if (mph > 0)
        {
            animated = true;
        }
    }

    private void setLockSpeed(float mph)
    {
        lock_anim.speed = mph;
    }

    public void resetAnim(float time)
    {
        object_anim.Play(object_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        object_anim.speed = 0;
    }

    public float animTime
    {
        get => object_anim.GetCurrentAnimatorStateInfo(0).normalizedTime * object_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public PlacePiece Place
    {
        get => placePiece;
        set => placePiece = value;
    }

    public ClampPiece Clamp
    {
        get => clampPiece;
        set => clampPiece = value;
    }
}