
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionVisibility : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    string collisionTag;

    [SerializeField]
    string gameObjectParentName;

    public Boolean TrainingWheels = false;

    GameObject sparks;

    [SerializeField] private float timerCountDown = 10f;
    [SerializeField] private Boolean reset = true;
    private bool isColliding = false;
    private float initialCountdown;

    void Start()
    {
        initialCountdown = timerCountDown;
        FindSpark();

    }

    public void FindSpark()
    {
        GameObject tmpObject = GameObject.Find(gameObjectParentName);
        if (tmpObject != null)
        {
            sparks = tmpObject.transform.GetChild(0).gameObject;
            if (sparks != null)
            {
                sparks.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isColliding == true)
        {
            sparks.SetActive(true);
            timerCountDown -= Time.deltaTime;
            if (timerCountDown < 0)
            {
                timerCountDown = 0;
                sparks.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collison)
    {
        //Debug.LogWarning("Collision Present");
        //Debug.LogWarning(collison.collider.tag);

        if (collison.collider.tag == collisionTag)
        {
            isColliding = true;
            
            sparks.transform.localPosition = collison.transform.position;
            if (!gameObject.GetComponent<Renderer>().enabled)
            {
                sparks.SetActive(false);
            } else
            {
                sparks.SetActive(true);
            }
        }
    }

    private void OnCollisionStay(Collision collison)
    {
        if (collison.collider.tag == collisionTag)
        {
            sparks.transform.position = collison.transform.position;
            if (timerCountDown <= 0)
            {
                if (TrainingWheels)
                {
                    gameObject.SetActive(false);
                }
                else
                {
                    gameObject.GetComponent<Renderer>().enabled = false;
                    Destroy(gameObject);
                    Destroy(this);
                    StaticRigidBody staticRigidBody = collison.transform.gameObject.GetComponent<StaticRigidBody>();
                    if(staticRigidBody != null)
                    {
                        staticRigidBody.endCollision();
                    }
                    BitCollisionController bitCollisionController = collison.transform.gameObject.GetComponent<BitCollisionController>();
                    if (bitCollisionController != null)
                    {
                        bitCollisionController.EndCollsion();
                    }
                    RevertDestruction revertDestruction = GetComponentInParent<RevertDestruction>();
                    if(revertDestruction != null)
                    {
                        revertDestruction.SaveTransform(transform.localPosition, transform.localRotation, transform.localScale);
                    }
                }
                sparks.SetActive(false);
            }

            if (!gameObject.GetComponent<Renderer>().enabled)
            {
                sparks.SetActive(false);
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == collisionTag)
        {
            if (reset)
            {
                timerCountDown = initialCountdown;
            }
            isColliding = false;
            sparks.SetActive(false);
        }
    }

    public string CollisionTag
    {
        get => collisionTag;
        set => collisionTag = value;
    }

    public float Countdown
    {
        get => initialCountdown;
        set => initialCountdown = value;
    }

    public float Timer
    {
        set => timerCountDown = value;
    }

    public string ParentName
    {
        get => gameObjectParentName;
        set => gameObjectParentName = value;
    }

}
