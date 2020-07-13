using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class CameraToggle : MonoBehaviour
{
    Camera[] cameras;
    [SerializeField] string genericCameraTag;
    private static string MAIN_CAM_TAG = "MainCamera"; 
    private int index;
    void Awake()
    {
        index = 0;
        cameras = GetComponentsInChildren<Camera>();
        UnityEngine.Debug.LogWarning("Cameras: " + cameras.Length.ToString());
        deactivateAll();
        SwtichView();
        
    }

    // Update is called once per frame
    public void SwtichView()
    {
        UnityEngine.Debug.LogWarning("Camera: " + index.ToString());

        if (index == cameras.Length)
        {
            cameras[cameras.Length - 1].tag = genericCameraTag;
            cameras[cameras.Length - 1].enabled = false;
            index = 0;
        } else if(index != 0)
        {
            cameras[index - 1].tag = genericCameraTag;
            cameras[index - 1].enabled = false;
        }
        cameras[index].enabled = true;
        cameras[index].tag = MAIN_CAM_TAG;
        index++;
    }

    void deactivateAll()
    {
        for(int i = 0; i<cameras.Length; i++)
        {
            cameras[i].enabled = false;
        }
    }
}
