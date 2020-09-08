using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YWheelControl : MonoBehaviour
{
    [SerializeField] private DRO_Manager manager;
    [SerializeField] private OperationSelection selector;
    [SerializeField] GameObject animObject, lockAnimObject;
    [SerializeField] public DRO_Button YLockButton;
    [SerializeField] GameObject wheel, lockHandle;
    [SerializeField] Boolean enable = true;
    [SerializeField] private float speedMultiplier, arrowSpeed;
    private PlacePiece placePiece;
    [SerializeField] private string lockBool, unlockBool;


    private float new_time, prev_time, prev_distance;
    public float currentSpeed;


    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    public Boolean forwardCollision, backwardCollision, locked, scrollActive, keyActive;
    public Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Awake()
    {
        prev_time = 0;
        prev_distance = 0;
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();

        forwardCollision = false;
        backwardCollision = false;
        locked = false;
        setSpeed(0.2f);
        pause();
        pauseLock();
        //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

        handle_enabled = true;
        wheel_spin = true;
    }

    public Boolean Locked
    {
        get => locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && !Input.GetKey(KeyCode.LeftShift) && !keyActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (YLockButton.Activated)
            {
                if(selector.Current.Name != "Null" && !selector.Current.GetPlacePiece().Clicked)
                {
                    WarningEvents.current.PieceFirst();
                    manager.resetDRO();
                }
            }
            if ((YLockButton.Activated == true) && !locked)
            {

                float distance, time;
                if (Input.mouseScrollDelta.y != 0)
                {
                    distance = Math.Abs(Input.mouseScrollDelta.y - prev_distance);
                    time = Math.Abs(Time.time - prev_time);
                    currentSpeed = (distance / time) * speedMultiplier;
                    scrollActive = true;
                }

                if (Input.mouseScrollDelta.y > 0f && !forwardCollision)
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
                    else {
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(currentSpeed);
                        //Debug.Log(Input.mouseScrollDelta.y);
                        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
                    }
                }
                else if (Input.mouseScrollDelta.y < 0f && !backwardCollision)
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
                    scrollActive = false;
                }
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (enable && !scrollActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (YLockButton.Activated)
            {
                if (selector.Current.Name != "Null" && !selector.Current.GetPlacePiece().Clicked)
                {
                    WarningEvents.current.PieceFirst();
                    manager.resetDRO();
                }
            }
            if ((YLockButton.Activated == true) && !locked)
            {

                if (e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow && !forwardCollision)
                {
                    keyActive = true;
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
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(arrowSpeed);
                        //Debug.Log(Input.mouseScrollDelta.y);
                        //Debug.Log(Input.GetAxis("Mouse ScrollWheel"));
                    }
                }
                else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow && !backwardCollision)
                {
                    keyActive = true;
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
                        setSpeed(arrowSpeed);
                    }
                }
                else
                {
                    pause();
                    keyActive = false;
                }
            }
        }
    }

    private void StopMovement()
    {
        pause();
        WarningEvents.current.StopTableMovement();
    }

    private void pause()
    {
        object_anim.speed = 0;
        animated = false;
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
        lock_anim.Play(lock_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        object_anim.speed = 0;
        lock_anim.speed = 0;
        lock_anim.SetBool(lockBool, true);
        lock_anim.SetBool(unlockBool, false);
        locked = false;
        forwardCollision = false;
        backwardCollision = false;
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