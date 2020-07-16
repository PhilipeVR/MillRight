using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ToggleText> toggleList;
    public LanguageSceneToggle languageScene;
    public DialogueManager toggleLangManager;
    public ComponentManager toggleComponentLangManager;
    public ImageToggle imageToggle;
    void Start()
    {
        if (languageScene.getLanguage())
        {
            toggleOnStart();
        }
    }

    private void toggleOnStart()
    {
        foreach(ToggleText toggleText in toggleList)
        {
            toggleText.toggle();
        }

        
        if (toggleLangManager != null)
        {
            toggleLangManager.switchLang();
        }
        if (imageToggle != null)
        {
            imageToggle.toggle();
        }
        if(toggleComponentLangManager != null)
        {
            toggleComponentLangManager.ChangeLanguage();
        }
    }

    public void updateLanguageState()
    {
        languageScene.setLanguage(!languageScene.getLanguage());
    }
}
