using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class Z_Axis_Lock_Animation : MonoBehaviour
{
    float MAX_HEIGHT;
    float MIN_HEIGHT;

    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField]
    List<GameObject> wheel_parts;

    [SerializeField]
    GameObject handle;

    [SerializeField]
    Boolean enable = true;

    [SerializeField]
    GameObject Spindle;

    Boolean animated = true;

    Boolean handle_enable, wheel_spin; 
    public Boolean collided;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update

    private void Awake()
    {

        if (enable)
        {

            collided = false;

            object_anim = animObject.GetComponent<Animator>();
            lock_anim = lockAnimObject.GetComponent<Animator>();


            MIN_HEIGHT = Spindle.transform.localPosition.y - 0.3f;
            MAX_HEIGHT = Spindle.transform.localPosition.y;

            setSpeed(0.1f);
            setLockSpeed(0.5f);
            pause();
            pauseLock();
            //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

            handle_enable = true;

        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (enable)
        {

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {

                    if (hit.collider.gameObject.Equals(handle))
                    {
                        //Debug.LogWarning("Handle is Clicked");
                        if (!animated)
                        {
                            handle_enable = !handle_enable;
                            //Debug.LogWarning("Handle is " + handle_enable.ToString());
                            if (!handle_enable)
                            {
                                lock_anim.SetFloat("Reverse", 1);
                                setLockSpeed(1f);
                                lock_anim.Play(lock_anim.runtimeAnimatorController.animationClips[0].name);
                            }
                            else
                            {
                                lock_anim.SetFloat("Reverse", -1);
                                setLockSpeed(1f);
                                lock_anim.Play(lock_anim.runtimeAnimatorController.animationClips[0].name);
                            }
                            wheel_spin = false;
                        }
                    }
                    else if (checkWheelList(hit.collider.gameObject))
                    {
                        //Debug.LogWarning("Wheel is Clicked");
                        if (!animated)
                        {
                            if (handle_enable)
                            {
                                wheel_spin = true;
                            }
                        }
                    }
                    else
                    {
                        wheel_spin = false;
                    }
                }

            }
            

            if (Input.mouseScrollDelta.y > 0f && wheel_spin && !collided)
            {
                Vector3 tmp_pos = Spindle.transform.localPosition;
                float y_pos = tmp_pos.y - 0.0005f;

                if (y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                {
                    Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);
                    Spindle.transform.localPosition = new_pos;
                    object_anim.SetFloat("Reverse", 1);
                    setSpeed(2f);
                }
            }
            else if (Input.mouseScrollDelta.y < 0f && wheel_spin)
            {
                Vector3 tmp_pos = Spindle.transform.localPosition;
                float y_pos = tmp_pos.y + 0.0005f;

                if(y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                {
                    Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);

                    Spindle.transform.localPosition = new_pos;
                    object_anim.SetFloat("Reverse", -1);
                    setSpeed(2f);
                }



            }
            else
            {
                pause();
            }
        }
    }

    private Boolean checkWheelList(GameObject obj)
    {
        foreach(GameObject part in wheel_parts)
        {
            if (obj.Equals(part))
            {
                return true;
            }
        }
        return false;
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
}
