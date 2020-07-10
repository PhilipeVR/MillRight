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
    GameObject quillFeedController, fineAdjustmentController;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.localPosition;
        init_rot = GetComponent<Rigidbody>().rotation;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //Debug.LogWarning("X: " + init_pos.x + ", Y: " + init_pos.y + ", Z: " + init_pos.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        updateStaticRigidBody();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("CubeStock"))
        {
            quillFeedController.GetComponent<QuillFeedControl>().collided = true;
            fineAdjustmentController.GetComponent<FineAdjustmentControl>().collided = true;
            Debug.LogWarning("HIT");
            updateStaticRigidBody();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag.Equals("CubeStock"))
        {
            updateStaticRigidBody();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("CubeStock"))
        {
            endCollision();
            Debug.LogWarning("No HIT");

        }
    }

    public void endCollision()
    {
        quillFeedController.GetComponent<QuillFeedControl>().collided = false;
        fineAdjustmentController.GetComponent<FineAdjustmentControl>().collided = false;
    }


    public void updateStaticRigidBody()
    {

        transform.localPosition = init_pos;
        GetComponent<Rigidbody>().rotation = init_rot;

    }
}
