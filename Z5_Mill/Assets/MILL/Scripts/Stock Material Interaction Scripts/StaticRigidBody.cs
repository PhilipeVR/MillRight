using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StaticRigidBody : MonoBehaviour
{

    Vector3 init_pos, spindle_pos;
    private Rigidbody rigidbody;
    Quaternion init_rot;
    [SerializeField] Toggle_On_Off PowerButton;
    [SerializeField] GameObject Spindle;
    [SerializeField] private string DrillingTag;
    [SerializeField] private QuillFeedControl quillFeed;
    [SerializeField] private XWheelControl xWheel;
    [SerializeField] private YWheelControl yWheel;
    [SerializeField] private FineAdjustmentControl fine;
    // Start is called before the first frame update
    void Start()
    {
        init_pos = transform.localPosition;
        rigidbody = GetComponent<Rigidbody>();
        init_rot = rigidbody.rotation;
        rigidbody.velocity = new Vector3(0, 0, 0);
        
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        updateStaticRigidBody();
       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.tag.Equals(DrillingTag) && PowerButton.PowerState)
        {
            WarningEvents.current.TurnOFF();
            PowerButton.EmergencyStop();
        }
        if (quillFeed.QuillLockButton.Activated || fine.FineAdjustmentButton.Activated)
        {
            quillFeed.collided = true;
            fine.collided = true;
        }
        else if (xWheel.XLockButton.Activated && xWheel.object_anim.GetFloat("Reverse") > 0)
        {
            xWheel.leftCollision = true;
        }
        else if (xWheel.XLockButton.Activated && xWheel.object_anim.GetFloat("Reverse") < 0)
        {
            xWheel.rightCollision = true;
        }
        else if (yWheel.YLockButton.Activated && yWheel.object_anim.GetFloat("Reverse") > 0)
        {
            yWheel.forwardCollision = true;
        }
        else if (yWheel.YLockButton.Activated && yWheel.object_anim.GetFloat("Reverse") < 0)
        {
            yWheel.backwardCollision = true;
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
        quillFeed.collided = false;
        fine.collided = false;
        xWheel.rightCollision = false;
        xWheel.leftCollision = false;
        yWheel.backwardCollision = false;
        yWheel.forwardCollision = false;
    }



    public void updateStaticRigidBody()
    {

        transform.localPosition = init_pos;
        rigidbody.rotation = init_rot;

    }
}
