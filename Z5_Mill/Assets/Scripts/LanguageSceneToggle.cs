using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageSceneToggle : MonoBehaviour
{
    public static Boolean language;
    // Start is called before the first frame update

    public void setLanguage(Boolean lang)
    {
        language = lang;
    }

    public Boolean getLanguage()
    {
        return language;
    }
}
