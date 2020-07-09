using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticRigidBody : MonoBehaviour
{
    Vector3 init_pos, spindle_pos;
    Quaternion init_rot;

    [SerializeField]
    GameObject Spindle;

    [SerializeField]
    GameObject axisController;
    // Start is called before the first frame update
    void Start()
    {
        spindle_pos = Spindle.transform.position;
        init_pos = GetComponent<Rigidbody>().position;
        init_rot = GetComponent<Rigidbody>().rotation;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //Debug.LogWarning("X: " + init_pos.x + ", Y: " + init_pos.y + ", Z: " + init_pos.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if (!spindle_pos.Equals(Spindle.transform.position))
        {
            init_pos = init_pos + new Vector3(0, Spindle.transform.position.y - spindle_pos.y,0);
            spindle_pos = Spindle.transform.position;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().MovePosition(init_pos);
        GetComponent<Rigidbody>().MoveRotation(init_rot);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("CubeStock"))
        {
            axisController.GetComponent<Z_Axis_Lock_Animation>().collided = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("CubeStock"))
        {
            axisController.GetComponent<Z_Axis_Lock_Animation>().collided = false;
        }
    }
}
