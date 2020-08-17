using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitCollisionController : MonoBehaviour
{

    private Vector3 init_pos;
    private Quaternion init_rot;
    [SerializeField] private ProcessAnimationController controller;
    [SerializeField] private string StockTag;

    private void Start()
    {
        init_pos = transform.localPosition;
        init_rot = GetComponent<Rigidbody>().rotation;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    }

    void FixedUpdate()
    {
        updateStaticRigidBody();

    }

    public void updateStaticRigidBody()
    {

        transform.localPosition = init_pos;
        GetComponent<Rigidbody>().rotation = init_rot;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == StockTag)
        {
            //Debug.Log("Im In A Collsion");
            controller.StartCollsion();
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == StockTag)
        {
            //Debug.Log("Im Still In A Collsion");
            controller.StartCollsion();
        }
    }


    public void EndCollsion()
    {
        //Debug.Log("Im Left the Collsion");
        controller.EndCollision();
    }
}
