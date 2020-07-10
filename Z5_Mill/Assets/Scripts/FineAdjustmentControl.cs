using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FineAdjustmentControl : MonoBehaviour
{

    float MAX_HEIGHT;
    float MIN_HEIGHT;

    [SerializeField]
    GameObject animObject;

    [SerializeField] DRO_ButtonState FineAdjustmentButton; // TEMPORARY --> THIS SHOULD BE FOR QUILL FEED ONLY

    [SerializeField]
    public GameObject fineAdjustment, prevAdjustment;

    [SerializeField]
    Boolean enable = true;

    public Boolean collided;
    Boolean animated, handle_enabled, wheel_spin;

    public float movementInterval = 0.001f;
    Animator object_anim, lock_anim;

    // Start is called before the first frame update
    void Start()
    {
        if (enable)
        {
            collided = false;
            object_anim = animObject.GetComponent<Animator>();

            MIN_HEIGHT = fineAdjustment.transform.localPosition.y - 0.01f;
            MAX_HEIGHT = fineAdjustment.transform.localPosition.y;

            prevAdjustment = fineAdjustment;


            setSpeed(0.2f);
            pause();
            //Debug.LogWarning(lock_anim.runtimeAnimatorController.animationClips[0].name);

            handle_enabled = true;
            wheel_spin = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enable)
        {
            if (!prevAdjustment.Equals(fineAdjustment))
            {
                MIN_HEIGHT = fineAdjustment.transform.localPosition.y - 0.01f;
                MAX_HEIGHT = fineAdjustment.transform.localPosition.y;
            }

            prevAdjustment = fineAdjustment;

            if (FineAdjustmentButton.checkIfEnabled == true)
            {

                if (Input.mouseScrollDelta.y > 0f && !collided)
                {
                    Debug.LogWarning("Scroll Up");

                    Vector3 tmp_pos = fineAdjustment.transform.localPosition;
                    float y_pos = tmp_pos.y - movementInterval;

                    if (y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                    {

                        Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);
                        fineAdjustment.transform.localPosition = new_pos;
                        object_anim.SetFloat("Reverse", 1);
                        setSpeed(2f);
                    }
                }
                else if (Input.mouseScrollDelta.y < 0f)
                {

                    Debug.LogWarning("Scroll Down");


                    Vector3 tmp_pos = fineAdjustment.transform.localPosition;
                    float y_pos = tmp_pos.y + movementInterval;

                    if (y_pos < MAX_HEIGHT && y_pos > MIN_HEIGHT)
                    {
                        Vector3 new_pos = new Vector3(tmp_pos.x, y_pos, tmp_pos.z);

                        fineAdjustment.transform.localPosition = new_pos;
                        object_anim.SetFloat("Reverse", -1);
                        setSpeed(2f);
                    }
                }
                else
                {
                    Debug.LogWarning("Nothing");
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


    private void setSpeed(float mph)
    {
        Debug.Log(mph);
        object_anim.speed = mph;
        if (mph > 0)
        {
            animated = true;
        }
    }

}
