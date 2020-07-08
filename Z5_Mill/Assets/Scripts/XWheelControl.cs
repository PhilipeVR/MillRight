using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class XWheelControl : MonoBehaviour
{
    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] private DRO_ButtonState xLockButton;

    [SerializeField]
    GameObject wheel, handle;

    [SerializeField]
    Boolean enabled = true;

    

    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Start()
    {
        object_anim = animObject.GetComponent<Animator>();
        lock_anim = lockAnimObject.GetComponent<Animator>();


        setSpeed(0.1f);
        setLockSpeed(0.5f);
        pause();
        pauseLock();
        //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

        handle_enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(xLockButton.enabled == true)
        {
            // Debug.Log("X LOCK ENABLED");
            if (Input.mouseScrollDelta.y > 0f)
            {
                object_anim.SetFloat("Reverse", 1);
                setSpeed(1f);
                Debug.Log("SCROLLING UP");
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                object_anim.SetFloat("Reverse", -1);
                setSpeed(1f);
                Debug.Log("SCROLLING DOWN");

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
