using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCylinderStock : MonoBehaviour
{

    private Vector3 init_pos;
    private Quaternion init_rot;
    [SerializeField] private Animator controller;
    [SerializeField] private string StockTag;
    [SerializeField] private float speed;

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
        if (collision.transform.tag == StockTag)
        {
            controller.SetFloat("Speed", 0);
        }

    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.tag == StockTag)
        {
            controller.SetFloat("Speed", 0);
        }
    }

    public void EndCollision()
    {
        controller.SetFloat("Speed", speed);
    }
}
