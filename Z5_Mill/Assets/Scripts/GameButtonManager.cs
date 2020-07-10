using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonManager : MonoBehaviour
{

    string drillTag, stopTag;

    [SerializeField]
    GameObject onButton, offButton;
    public GameObject animObject;
    public GameObject currentBit;

    public Boolean state;
    Animator anim_object, on_anim, off_anim;

    //Start is called before the first frame update
    void Start()
    {
        state = false;
        stopTag = currentBit.tag;
        anim_object = animObject.GetComponent<Animator>();
        on_anim = onButton.GetComponent<Animator>();
        off_anim = offButton.GetComponent<Animator>();

        anim_object.speed = 0;
        on_anim.speed = 0;
        off_anim.speed = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!state)
        {
            stopTag = currentBit.tag;
        }

    }

    public void turnOn()
    {
        anim_object.speed = 2f;
        on_anim.speed = 2f;
        on_anim.Play(on_anim.runtimeAnimatorController.animationClips[0].name);
        currentBit.tag = drillTag;
        state = true;
        Debug.LogWarning("On Button");

    }

    public void turnOff()
    {
        currentBit.tag = stopTag;
        off_anim.speed = 2f;
        off_anim.Play(off_anim.runtimeAnimatorController.animationClips[0].name);
        anim_object.speed = 0;
        state = false;
        Debug.LogWarning("Off Button");
    }

 
}
