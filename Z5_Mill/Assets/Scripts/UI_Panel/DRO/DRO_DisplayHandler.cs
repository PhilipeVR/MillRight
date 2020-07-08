using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_DisplayHandler : MonoBehaviour
{
    [SerializeField] private Transform vise; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform spindle; // point of reference for Z-DRO
    
    [SerializeField] private Text xText;
    [SerializeField] private Text yText;
    [SerializeField] private Text zText;

    void Update()
    {
        // Need to convert distance from "metres" to "inch" or "mm"
        // Also make this update only when there is a change in these coords

        Debug.Log(vise.position.x);
        Debug.Log(vise.position.y);
        Debug.Log(spindle.position.z);

        xText.text = vise.position.x.ToString();
        yText.text = vise.position.y.ToString();
        zText.text = spindle.position.z.ToString();
    }

}
