using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticRigidBody : MonoBehaviour
{

    Vector3 init_pos, spindle_pos;
    Quaternion init_rot;
    [SerializeField] Toggle_On_Off PowerButton;
    [SerializeField] GameObject Spindle;
    [SerializeField] private string DrillingTag;
    [SerializeField] GameObject quillFeedController, fineAdjustmentController, XWheelController, YWheelController;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.localPosition;
        init_rot = GetComponent<Rigidbody>().rotation;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        updateStaticRigidBody();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!collision.collider.tag.Equals(DrillingTag) && PowerButton.PowerState)
        {
            WarningEvents.current.TurnOFF();
            PowerButton.EmergencyStop();
        }
        if (quillFeedController.GetComponent<QuillFeedControl>().QuillLockButton.Activated || fineAdjustmentController.GetComponent<FineAdjustmentControl>().FineAdjustmentButton.Activated)
        {
            quillFeedController.GetComponent<QuillFeedControl>().collided = true;
            fineAdjustmentController.GetComponent<FineAdjustmentControl>().collided = true;
        }
        else if (XWheelController.GetComponent<XWheelControl>().XLockButton.Activated && XWheelController.GetComponent<XWheelControl>().object_anim.GetFloat("Reverse") > 0)
        {
            XWheelController.GetComponent<XWheelControl>().leftCollision = true;
        }
        else if (XWheelController.GetComponent<XWheelControl>().XLockButton.Activated && XWheelController.GetComponent<XWheelControl>().object_anim.GetFloat("Reverse") < 0)
        {
            XWheelController.GetComponent<XWheelControl>().rightCollision = true;
        }
        else if (YWheelController.GetComponent<YWheelControl>().YLockButton.Activated && YWheelController.GetComponent<YWheelControl>().object_anim.GetFloat("Reverse") > 0)
        {
            YWheelController.GetComponent<YWheelControl>().forwardCollision = true;
        }
        else if (YWheelController.GetComponent<YWheelControl>().YLockButton.Activated && YWheelController.GetComponent<YWheelControl>().object_anim.GetFloat("Reverse") < 0)
        {
            YWheelController.GetComponent<YWheelControl>().backwardCollision = true;
        }
        updateStaticRigidBody();
        
    }

    private void OnCollisionStay(Collision collision)
    {
        updateStaticRigidBody();
        
    }

    private void OnCollisionExit(Collision collision)
    {
        
        endCollision();
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
