using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    [SerializeField]
    float multiplier;

    public void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(Physics.gravity*multiplier, ForceMode.Acceleration);
    }
}
