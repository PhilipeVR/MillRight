using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class LockAnimationScript : MonoBehaviour
{
    [SerializeField]
    GameObject animObject;

    [SerializeField]
    GameObject wheel, handle;

    [SerializeField]
    Boolean enable = true;

    Boolean animated = true;
    Boolean handle_enable;
    float prev_speed;
    Animator object_anim;
    
    // Start is called before the first frame update
    void Start()
    {
        if (enable)
        {
            object_anim = animObject.GetComponent<Animator>();
            setSpeed(0.1f);
            prev_speed = object_anim.speed;
            pause();

            handle_enable = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enable)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.Equals(wheel))
                {
                    if (handle_enable)
                    {
                        toggleAnimation();
                    }
                    Debug.LogWarning("Wheel is Clicked");
                }
                else if (hit.collider.gameObject.Equals(handle))
                {
                    Debug.LogWarning("Handle is Clicked");
                    if (!animated)
                    {
                        handle_enable = !handle_enable;
                        Debug.LogWarning("Handle is " + handle_enable.ToString());
                    }
                }
            }
        }
    }

    private void toggleAnimation()
    {
        if (animated)
        {
            pause();
        }
        else
        {
            play();
        }
    }

    private void pause()
    {
        Debug.LogWarning("Animator Pause");
        object_anim.speed = 0;
        animated = false;
    }

    private void play()
    {
        Debug.LogWarning("Animator Play");
        object_anim.speed = prev_speed;
        animated = true;
    }

    private void setSpeed(float mph)
    {
        object_anim.speed = mph;
    }

}
