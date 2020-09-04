using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuillFeedControl : MonoBehaviour
{

    float MAX_HEIGHT;
    float MIN_HEIGHT;

    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] public DRO_Button QuillLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    public Boolean collided, moving, prev_state;

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
            //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

            handle_enabled = true;
            wheel_spin = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enable && !Input.GetKey(KeyCode.LeftShift)) // Do not execute when left shift held down (to not interfere with camera controller)
        {
            if (QuillLockButton.Activated == true && wheel.activeSelf)
            {

                if (Input.mouseScrollDelta.y > 0f && !collided)
                {
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
        wheel.transform.localPosition = new Vector3(wheel.transform.localPosition.x, wheel.transform.localPosition.y, MAX_HEIGHT);
        collided = false;

    }

    public float animTime
    {
        get => object_anim.GetCurrentAnimatorStateInfo(0).normalizedTime * object_anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    public float Height
    {
        get => wheel.transform.localPosition.y;
    }

    public float MaxHeight
    {
        get => MAX_HEIGHT;
    }
}