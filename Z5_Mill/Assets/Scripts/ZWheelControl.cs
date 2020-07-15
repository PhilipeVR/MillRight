﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

public class ZWheelControl : MonoBehaviour
{
    [SerializeField]
    GameObject animObject, lockAnimObject;

    [SerializeField] DRO_ButtonState ZLockButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    GameObject wheel, lockHandle;

    [SerializeField]
    Boolean enable = true;

    

    Boolean animated = true;
    Boolean handle_enabled, wheel_spin;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Start()
    {
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
        if(ZLockButton.checkIfEnabled == true)
        {
            if (Input.mouseScrollDelta.y > 0f)
            {
                object_anim.SetFloat("Reverse", 1);
                setSpeed(0.2f);
            }
            else if (Input.mouseScrollDelta.y < 0f)
            {
                object_anim.SetFloat("Reverse", -1);
                setSpeed(0.2f);
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
}