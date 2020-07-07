using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class LockAnimationScript : MonoBehaviour
{
    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField]
    GameObject wheel, handle;

    [SerializeField]
    Boolean enable = true;

    

    Boolean animated = true;
    Boolean handle_enable, wheel_spin;
    Animator object_anim, lock_anim;
    
    // Start is called before the first frame update
    void Start()
    {
        if (enable)
        {
            object_anim = animObject.GetComponent<Animator>();
            lock_anim = lockAnimObject.GetComponent<Animator>();


            setSpeed(0.1f);
            setLockSpeed(0.5f);
            pause();
            pauseLock();
            //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

            handle_enable = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.Equals(wheel))
                    {
                        if (handle_enable)
                        {
                            wheel_spin = true;
                        }
                        //Debug.LogWarning("Wheel is Clicked");
                    }
                    else if (hit.collider.gameObject.Equals(handle))
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
                    } else
                    {
                        wheel_spin = false;
                    }
                }
            }

            if (Input.mouseScrollDelta.y > 0f && wheel_spin)
            {
                object_anim.SetFloat("Reverse", 1);
                setSpeed(0.1f);
            }
            else if (Input.mouseScrollDelta.y < 0f && wheel_spin)
            {
                object_anim.SetFloat("Reverse", -1);
                setSpeed(0.1f);

            } else
            {
                pause();
            }
        }
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
