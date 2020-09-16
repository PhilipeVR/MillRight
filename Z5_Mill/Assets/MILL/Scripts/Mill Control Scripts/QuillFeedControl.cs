using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuillFeedControl : MonoBehaviour
{

    public static float MAX_HEIGHT;
    public static float MIN_HEIGHT;

    [SerializeField] private FineAdjustmentControl fineControl;
    [SerializeField] private Toggle_On_Off powerBTN;
    [SerializeField] private OperationSelection selector;


    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] public DRO_Button QuillLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    public Boolean collided, moving, prev_state, reminder, scrollActive, keyActive;

    [SerializeField] private float movementInterval;

    private PlacePiece placePiece;
    private ClampPiece clampPiece;


    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Awake()
    {
        if (enable)
        {
            reminder = false;
            moving = false;
            collided = false;
            object_anim = animObject.GetComponent<Animator>();
            lock_anim = lockAnimObject.GetComponent<Animator>();

            MIN_HEIGHT = wheel.transform.localPosition.z - 0.15f;
            MAX_HEIGHT = wheel.transform.localPosition.z;


            setSpeed(0.2f);
            setLockSpeed(0.5f);
            pause();
            pauseLock();

            handle_enabled = true;
            wheel_spin = true;
            fineControl.UpdateLimit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && !Input.GetKey(KeyCode.LeftShift) && !keyActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (QuillLockButton.Activated == true && wheel.activeSelf)
            {

                RemindUser();

                if (Input.mouseScrollDelta.y > 0f && !collided)
                {
                    scrollActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        Vector3 tmp_pos = wheel.transform.localPosition;
                        float z_pos = tmp_pos.z - movementInterval;

                        if (z_pos <= MAX_HEIGHT && z_pos >= MIN_HEIGHT)
                        {
                            moving = true;

                            Vector3 new_pos = new Vector3(tmp_pos.x, tmp_pos.y, z_pos);
                            wheel.transform.localPosition = new_pos;
                            object_anim.SetFloat("Reverse", 1);
                            setSpeed(2f);
                        }
                    }
                }
                else if (Input.mouseScrollDelta.y < 0f)
                {
                    scrollActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        Vector3 tmp_pos = wheel.transform.localPosition;
                        float z_pos = tmp_pos.z + movementInterval;

                        if (z_pos <= MAX_HEIGHT && z_pos >= MIN_HEIGHT)
                        {
                            moving = true;

                            Vector3 new_pos = new Vector3(tmp_pos.x, tmp_pos.y, z_pos);

                            wheel.transform.localPosition = new_pos;
                            object_anim.SetFloat("Reverse", -1);
                            setSpeed(2f);
                        }
                    }
                }
                else
                {
                    scrollActive = false;
                    moving = false;
                    pause();
                }
            }
        }
    }

    private void OnGUI()
    {
        Event e = Event.current;
        if (enable && !scrollActive) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (QuillLockButton.Activated == true && wheel.activeSelf)
            {

                RemindUser();

                if (e.type == EventType.KeyDown && e.keyCode == KeyCode.DownArrow && !collided)
                {
                    keyActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        Vector3 tmp_pos = wheel.transform.localPosition;
                        float z_pos = tmp_pos.z - movementInterval;

                        if (z_pos <= MAX_HEIGHT && z_pos >= MIN_HEIGHT)
                        {
                            moving = true;

                            Vector3 new_pos = new Vector3(tmp_pos.x, tmp_pos.y, z_pos);
                            wheel.transform.localPosition = new_pos;
                            object_anim.SetFloat("Reverse", 1);
                            setSpeed(2f);
                        }
                    }
                }
                else if (e.type == EventType.KeyDown && e.keyCode == KeyCode.UpArrow)
                {
                    keyActive = true;
                    Boolean testPlace = placePiece != null;
                    Boolean testClamp = clampPiece != null;
                    if ((testPlace && placePiece.animTime > 0f && placePiece.animTime < 1f) || (testClamp && clampPiece.animTime > 0f && clampPiece.animTime < 1f))
                    {
                        StopMovement();
                    }
                    else
                    {
                        Vector3 tmp_pos = wheel.transform.localPosition;
                        float z_pos = tmp_pos.z + movementInterval;

                        if (z_pos <= MAX_HEIGHT && z_pos >= MIN_HEIGHT)
                        {
                            moving = true;

                            Vector3 new_pos = new Vector3(tmp_pos.x, tmp_pos.y, z_pos);

                            wheel.transform.localPosition = new_pos;
                            object_anim.SetFloat("Reverse", -1);
                            setSpeed(2f);
                        }
                    }
                }
                else
                {
                    keyActive = false;
                    moving = false;
                    pause();
                }
            }
        }
    }

    private void pause()
    {
        object_anim.speed = 0;
        animated = false;
    }

    private void StopMovement()
    {
        pause();
        WarningEvents.current.CutterNear();
    }

    private void pauseLock()
    {
        lock_anim.speed = 0;
    }

    private void setSpeed(float mph)
    {
        object_anim.speed = mph;
        if (mph > 0)
        {
            animated = true;
        }
    }

    private void RemindUser()
    {
        if (powerBTN.isON && !reminder && (selector.Current.Name == selector.FaceMill.Name))
        {
            WarningEvents.current.ZeroZ();
            reminder = true;
        }
    }

    private void setLockSpeed(float mph)
    {
        lock_anim.speed = mph;
    }

    public void resetAnim(float time)
    {
        object_anim.Play(object_anim.runtimeAnimatorController.animationClips[0].name, 0, time);
        wheel.transform.localPosition = new Vector3(wheel.transform.localPosition.x, wheel.transform.localPosition.y, MAX_HEIGHT);
        collided = false;
        reminder = false;

    }

    public float animTime
    {
        get => object_anim.GetCurrentAnimatorStateInfo(0).normalizedTime * object_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public float Height
    {
        get => wheel.transform.localPosition.z;
    }

    public float MaxHeight
    {
        get => MAX_HEIGHT;
    }

    public float MinHeight
    {
        get => MIN_HEIGHT;
    }
}