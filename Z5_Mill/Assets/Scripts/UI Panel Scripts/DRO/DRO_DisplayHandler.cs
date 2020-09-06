using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_DisplayHandler : MonoBehaviour
{
    [SerializeField] private Transform worktable; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform table; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform spindle, spindle2; // point of reference for Z-DRO
    
    [SerializeField] private Text xText;
    [SerializeField] private Text yText;
    [SerializeField] private Text zText;

    [SerializeField] private DRO_Button inchButton;
    [SerializeField] private DRO_Button mmButton;
    [SerializeField] private DRO_Button zeroButton;

    private float x0, x;
    private float y0, y;
    private float z0, z;
    private float z02, z2;

    private float starterX, starterY, starterZ, starterZ2;


    private float inch = 39.37f; // 39.37 inch per m
    private float mm = 1000.0f; // 1000 mm per m
    private float unitConversion;

    void Start()
    {
        unitConversion = inch; //default setting
        starterX = x0 =  worktable.localPosition.x * unitConversion;
        starterY = y0 =  table.localPosition.y * unitConversion;
        starterZ = z0 =  spindle.localPosition.z * unitConversion;
        if (spindle2 != null)
        {
            starterZ2 = z02 = spindle2.localPosition.z * unitConversion;
        }
    }

    public void ResetZero()
    {
        x0 = starterX;
        y0 = starterY;
        z0 = starterZ;
        z02 = starterZ2;
    }



    void Update()
    {
        // Need to convert distance from "metres" to "inch" or "mm"
        // Also make this update only when there is a change in these coords

        if(inchButton.Activated)
        {
            unitConversion = inch;
            //Debug.Log("Inch");
        }
        if(mmButton.Activated)
        {
            unitConversion = mm;
        }


        //Debug.Log("Z0: " + z0.ToString());
        //Debug.Log("Mill Spindle Local Position: " + spindle.localPosition.y.ToString());

        x =  worktable.localPosition.x * unitConversion - x0;
        y =  table.localPosition.y * unitConversion - y0;
        if (spindle2 != null)
        {
            z = (spindle.localPosition.z * unitConversion * (-1) - z0) + (spindle2.localPosition.z * unitConversion * (-1) + z02);
        }
        else
        {
            z = (spindle.localPosition.z * unitConversion * (-1) - z0);
        }
        xText.text = x.ToString("0.0000");
        yText.text = y.ToString("0.0000");
        zText.text = z.ToString("0.0000");

    }

    public void zero(string axis)
    {
        if(axis == "x")
        {
            x0 = worktable.localPosition.x*unitConversion;
        }
        if(axis == "y")
        {
            y0 = table.localPosition.y * unitConversion;
        }
        if(axis == "z")
        {
            z0 = spindle.localPosition.z * unitConversion * (-1);
            if (spindle2 != null)
            {
                z02 = spindle2.localPosition.z * unitConversion;
            }
        }
        

    }


}
