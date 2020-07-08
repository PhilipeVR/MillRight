using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameButtonManager : MonoBehaviour
{

    [SerializeField]
    string drillTag, stopTag;

    [SerializeField]
    GameObject onButton, offButton, animObject;

    Boolean state;
    Animator anim_object, on_anim, off_anim;

    // Start is called before the first frame update
    void Start()
    {
        state = false;
        animObject.transform.GetChild(0).gameObject.tag = stopTag;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.Equals(onButton))
                {
                    if (!state)
                    {
                        turnOn();
                    }
                } else if (hit.collider.gameObject.Equals(offButton))
                {
                    if (state)
                    {
                        turnOff();
                    }
                }
            }
        }
    }

    void turnOn()
    {
        anim_object.speed = 2f;
        on_anim.speed = 2f;
        on_anim.Play(on_anim.runtimeAnimatorController.animationClips[0].name);
        animObject.transform.GetChild(0).gameObject.tag = drillTag;
        state = true;
        Debug.LogWarning("On Button");

    }

    void turnOff()
    {
        animObject.transform.GetChild(0).gameObject.tag = stopTag;
        off_anim.speed = 2f;
        off_anim.Play(off_anim.runtimeAnimatorController.animationClips[0].name);
        anim_object.speed = 0;
        state = false;
        Debug.LogWarning("Off Button");
    }

 
}
