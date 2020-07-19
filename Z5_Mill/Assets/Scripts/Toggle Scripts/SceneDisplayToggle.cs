using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneDisplayToggle : MonoBehaviour
{
    public static Boolean tutorial;
    // Start is called before the first frame update

    public void setTutorial(Boolean tut)
    {
        tutorial = tut;
    }

    public Boolean getTutorial()
    {
        return tutorial;
    }
}
