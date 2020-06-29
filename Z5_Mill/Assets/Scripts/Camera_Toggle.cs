using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Toggle : MonoBehaviour
{
    public Camera cam_drill1;
    public Camera cam_drill2;
    public Camera cam_sm1; // sm == side milling perspective
    public Camera cam_fm1; // fm == face milling perspective 
    public Camera cam_main;

    public void switchcam(int x) {
        deactivateall ();
        if (x==1)
        {
            cam_drill1.enabled = true;
        }
        else if (x==2)
        {
            cam_drill2.enabled = true;
        }
        else if (x==3) 
        {
            cam_sm1.enabled = true;
        }
        else if (x==4)
        {
            cam_fm1.enabled = true;
        }
        else if (x==5)
        {
            cam_main.enabled = true;
        }
    }

    public void deactivateall() {
        cam_drill1.enabled = false;
        cam_drill2.enabled = false;
        cam_sm1.enabled = false;
        cam_fm1.enabled = false;
        cam_main.enabled = false;
    }

}
