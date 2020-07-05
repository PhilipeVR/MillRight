using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddPlayerControlledVelocity : MonoBehaviour
{
    [SerializeField]
    Vector3 vectorVelocity;

    [SerializeField]
    KeyCode keyPlus;

    [SerializeField]
    KeyCode keyMinus;

    // Update is called once per frame
    void FixedUpdate(){
        if (Input.GetKey(keyPlus))
            GetComponent<Rigidbody>().velocity += vectorVelocity;
       
        if (Input.GetKey(keyMinus))
            GetComponent<Rigidbody>().velocity -= vectorVelocity;
        
    }
}
