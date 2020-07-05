using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class CollisionVisibility : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    string collisionTag;

    GameObject sparks;

    private float timerCountDown = 5.0f;
    private bool isColliding = false;

    void Start()
    {

        sparks = GameObject.Find("Cube Complete (Mesh)").transform.GetChild(0).gameObject;
        if (sparks != null)
        {
            sparks.SetActive(false);
        } else
        {
            Debug.LogError("NullPointerException");
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
        if (collison.collider.tag == collisionTag)
        {
            isColliding = true;
            
            sparks.transform.position = collison.transform.position;
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
                gameObject.GetComponent<Renderer>().enabled = false;
                Destroy(gameObject);
                Destroy(this);
                sparks.SetActive(false);
            }

            if(!gameObject.GetComponent<Renderer>().enabled)
            {
                sparks.SetActive(false);
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == collisionTag)
        {
            isColliding = false;
            timerCountDown = 5.0f;
            sparks.SetActive(false);
        }
    }

}
