using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_DisplayHandler : MonoBehaviour
{
    [SerializeField] private Transform worktable; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform table; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform spindle; // point of reference for Z-DRO
    
    [SerializeField] private Text xText;
    [SerializeField] private Text yText;
    [SerializeField] private Text zText;

    [SerializeField] private DRO_ButtonState inchButton;
    [SerializeField] private DRO_ButtonState mmButton;

    private float x0, x;
    private float y0, y;
    private float z0, z;

    private float inch = 39.37f; // 39.37 inch per m
    private float mm = 1000.0f; // 1000 mm per m
    private float unitConversion;

    void Start()
    {
        unitConversion = inch; //default setting
        x0 =  worktable.localPosition.x * unitConversion;
        y0 =  table.localPosition.y * unitConversion;
        z0 =  spindle.position.z * unitConversion;
    }
    
    void Update()
    {
        // Need to convert distance from "metres" to "inch" or "mm"
        // Also make this update only when there is a change in these coords

        if(inchButton.checkIfEnabled)
        {
            unitConversion = inch;
            //Debug.Log("Inch");
        }
        if(mmButton.checkIfEnabled)
        {
            unitConversion = mm;
        }


        x =  worktable.localPosition.x * unitConversion - x0;
        y =  table.localPosition.y * unitConversion - y0;
        z =  spindle.position.z * unitConversion -z0;

        xText.text = x.ToString("0.00000");
        yText.text = y.ToString("0.00000");
        zText.text = z.ToString("0.00000");

    }

}
