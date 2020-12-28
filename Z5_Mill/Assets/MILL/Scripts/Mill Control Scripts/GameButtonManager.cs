
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonManager : MonoBehaviour
{

    string stopTag;
    public string drillTag;

    [SerializeField]
    public GameObject animObject;
    public GameObject currentBit;
    [SerializeField] private RampDownSpeedMill ramp;
    [SerializeField] private String rotationParamName;

    public Boolean state;
    Animator anim_object, on_anim, off_anim;

    //Start is called before the first frame update
    void Start()
    {
        state = false;
        stopTag = currentBit.tag;
        anim_object = animObject.GetComponent<Animator>();
        anim_object.speed = 0;
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
        anim_object.SetFloat(rotationParamName, 1f);
        currentBit.tag = drillTag;
        state = true;
    }

    public void turnOff()
    {
        currentBit.tag = stopTag;
        ramp.RampDown();
        state = false;
    }

 
}
