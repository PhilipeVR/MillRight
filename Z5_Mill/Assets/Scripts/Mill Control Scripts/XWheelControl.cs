using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using UnityEngine;

public class XWheelControl : MonoBehaviour
{
    [SerializeField] private Toggle_On_Off powerBTN;
    [SerializeField] private DRO_Manager manager;
    [SerializeField] private OperationSelection selector;
    [SerializeField] GameObject animObject, lockAnimObject;
    [SerializeField] public DRO_Button XLockButton;
    [SerializeField] GameObject wheel, lockHandle;
    [SerializeField] Boolean enable = true;
    [SerializeField] private float speedMultiplier;
    private PlacePiece placePiece;

    private float new_time, prev_time, prev_distance;
    public float currentSpeed;
    [SerializeField] private string lockBool, unlockBool;

    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    public Boolean leftCollision, rightCollision, locked;
    public Animator object_anim, lock_anim;
    private Boolean reminder;

    // Start is called before the first frame update
    void Awake()
    {
        reminder = false;
        prev_time = 0;
        prev_distance = 0;
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();
        leftCollision = false;
        rightCollision = false;
        pauseLock();
        locked = false;
        pause();
        //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

        handle_enabled = true;
        wheel_spin = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && !Input.GetKey(KeyCode.LeftShift)) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (XLockButton.Activated)
            {
                if (selector.Current.Name != "Null" && !selector.Current.GetPlacePiece().Clicked)
                {
                    WarningEvents.current.PieceFirst();
                    manager.resetDRO();
                }
            }
            if ((XLockButton.Activated == true) && !locked)
            {
                RemindUser();

                float distance, time;
                if (Input.mouseScrollDelta.y != 0)
                {
                    distance = Math.Abs(Input.mouseScrollDelta.y - prev_distance);
                    time = Math.Abs(Time.time - prev_time);
                    currentSpeed = (distance / time) * speedMultiplier;
                }


                if (Input.mouseScrollDelta.y > 0f && !leftCollision)
                {

                    Boolean testPlace = placePiece != null;
                    if(testPlace && !placePiece.Clicked)
                    {
                        pause();
                        WarningEvents.current.PieceFirst();
                    }
                    else if (testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f)
                    {
                        StopMovement();
                    }

                    else
                    {
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(currentSpeed);
                        //Debug.Log(Input.mouseScrollDelta.y);
                        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
                    }
                }
                else if (Input.mouseScrollDelta.y < 0f && !rightCollision)
                {

                    Boolean testPlace = placePiece != null;
                    if (testPlace && !placePiece.Clicked)
                    {
                        pause();
                        WarningEvents.current.PieceFirst();
                    }
                    else if (testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f)
                    {
                        StopMovement();
                    }
                    else
                    {
                        object_anim.SetFloat("Reverse", -1);
                        setSpeed(currentSpeed);
                    }
                }
                else
                {
                    pause();
                }
            }
        }
    }

    private void RemindUser()
    {
        if (powerBTN.isON && !reminder && (selector.Current.Name == selector.SideMill.Name)) 
        {
            WarningEvents.current.LockX();
            reminder = true;
        }
    }

    private void StopMovement()
    {
        pause();
        WarningEvents.current.StopTableMovement();
    }

    public Boolean Locked
    {
        get => locked;
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

    public void ToggleLock(float speed)
    {
        if (locked)
        {
            lock_anim.SetBool(lockBool, false);
            lock_anim.SetBool(unlockBool, true);
        }
        else
        {
            lock_anim.SetBool(lockBool, true);
            lock_anim.SetBool(unlockBool, false);
        }
        locked = !locked;
        setLockSpeed(speed);
    }

    private void setLockSpeed(float mph)
    {
        lock_anim.speed = mph;
    }

    public void resetAnim(float time)
    {
        object_anim.Play(object_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        lock_anim.Play(lock_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        object_anim.speed = 0;
        lock_anim.speed = 0;
        lock_anim.SetBool(lockBool, true);
        lock_anim.SetBool(unlockBool, false);
        locked = false;
        leftCollision = false;
        rightCollision = false;
        reminder = false;
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
}