using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSceneSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public List<ToggleText> toggleList;
    public ToolTip toggleHint;
    public LanguageSceneToggle languageScene;
    public DialogueManager toggleLangManager;
    public WarningManager warningLangManager;
    public ComponentManager toggleComponentLangManager;
    public ImageToggle imageToggle;
    public AdditionalInfoManager toolHolderManager;
    public ToggleScene toggleScene;
    public VideoProcessManager videoProcessManager;
    public VideoStandardController videoStandardController;
    public VideoController videoController;
    public YoutubeExceptionListener youtubeExceptionListener;
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
        if(toolHolderManager != null)
        {
            toolHolderManager.ChangeLanguage();
        }
        if(toggleHint != null)
        {
            toggleHint.toggleLang();
        }
        if(warningLangManager != null)
        {
            warningLangManager.SwitchLang();
        }
        if(toggleScene != null)
        {
            toggleScene.SwitchLang();
        }
        if (videoController != null)
        {
            videoController.SwitchLang();
        }
        if(videoProcessManager != null)
        {
            videoProcessManager.SwitchLang();
        }
        if(videoStandardController != null)
        {
            videoStandardController.SwitchLang();
        }
    }

    public void updateLanguageState()
    {
        languageScene.setLanguage(!languageScene.getLanguage());
    }
}
