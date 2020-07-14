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
    GameObject quillFeedController, fineAdjustmentController, XWheelController, YWheelController;
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
            if (quillFeedController.GetComponent<QuillFeedControl>().QuillLockButton.checkIfEnabled || fineAdjustmentController.GetComponent<FineAdjustmentControl>().FineAdjustmentButton.checkIfEnabled)
            {
                quillFeedController.GetComponent<QuillFeedControl>().collided = true;
                fineAdjustmentController.GetComponent<FineAdjustmentControl>().collided = true;
            }
            else if(XWheelController.GetComponent<XWheelControl>().XLockButton.checkIfEnabled && XWheelController.GetComponent<XWheelControl>().object_anim.GetFloat("Reverse") > 0)
            {
                XWheelController.GetComponent<XWheelControl>().leftCollision = true;
            }
            else if (XWheelController.GetComponent<XWheelControl>().XLockButton.checkIfEnabled && XWheelController.GetComponent<XWheelControl>().object_anim.GetFloat("Reverse") < 0)
            {
                XWheelController.GetComponent<XWheelControl>().rightCollision = true;
            }
            else if (YWheelController.GetComponent<YWheelControl>().YLockButton.checkIfEnabled && YWheelController.GetComponent<YWheelControl>().object_anim.GetFloat("Reverse") > 0)
            {
                YWheelController.GetComponent<YWheelControl>().forwardCollision = true;
            }
            else if (YWheelController.GetComponent<YWheelControl>().YLockButton.checkIfEnabled && YWheelController.GetComponent<YWheelControl>().object_anim.GetFloat("Reverse") < 0)
            {
                YWheelController.GetComponent<YWheelControl>().backwardCollision = true;

            }
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
        XWheelController.GetComponent<XWheelControl>().rightCollision = false;
        XWheelController.GetComponent<XWheelControl>().leftCollision = false;
        YWheelController.GetComponent<YWheelControl>().backwardCollision = false;
        YWheelController.GetComponent<YWheelControl>().forwardCollision = false;
    }


    public void updateStaticRigidBody()
    {

        transform.localPosition = init_pos;
        GetComponent<Rigidbody>().rotation = init_rot;

    }
}
