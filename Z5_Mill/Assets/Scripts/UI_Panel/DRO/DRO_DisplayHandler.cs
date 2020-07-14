using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DRO_DisplayHandler : MonoBehaviour
{
    [SerializeField] private Transform worktable; // point of reference for X-DRO and Y-DRO
    [SerializeField] private Transform spindle; // point of reference for Z-DRO
    
    [SerializeField] private Text xText;
    [SerializeField] private Text yText;
    [SerializeField] private Text zText;

    [SerializeField] private DRO_ButtonState inchButton;
    [SerializeField] private DRO_ButtonState mmButton;

    private float x;
    private float y;
    private float z;

    private float inch = 39.37f; // 39.37 inch per m
    private float mm = 1000.0f; // 1000 mm per m
    private float unitConversion;
    private float scaleFactor = 0.1f; // 1 unit in Unity is equivalient to 0.1 m

    void Start()
    {
        unitConversion = inch; //default setting
    }
    
    void Update()
    {
        // Need to convert distance from "metres" to "inch" or "mm"
        // Also make this update only when there is a change in these coords

        if(inchButton.checkIfEnabled)
        {
            unitConversion = inch;
            Debug.Log("Inch");
        }
        if(mmButton.checkIfEnabled)
        {
            unitConversion = mm;
        }


        x =  worktable.localPosition.x / scaleFactor / unitConversion;
        y =  worktable.localPosition.y / scaleFactor / unitConversion;
        z =  spindle.localPosition.z / scaleFactor / unitConversion;
        
        xText.text = x.ToString("0.00000");
        yText.text = y.ToString("0.00000");
        zText.text = z.ToString("0.00000");

    }

}
