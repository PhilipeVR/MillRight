﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScene : MonoBehaviour
{
    [SerializeField] private string frenchLoadQuiz, englishLoadQuiz;
    [SerializeField] private LoadSceneScript loadScene;
    [SerializeField] public string currentLoadQuiz;
    private Boolean language = false;
    public void SwitchLang()
    {
        if (!language)
        {
            currentLoadQuiz = frenchLoadQuiz;
        }   
        else
        {
            currentLoadQuiz = englishLoadQuiz;
        }
        language = !language;
    }

    // Update is called once per frame
    public void LoadScene()
    {
        loadScene.loadlevel(currentLoadQuiz);
    }

    private void OnGUI()
    {
        Debug.Log("First Closer");

        Event e = Event.current;
        if (e.type == EventType.KeyDown && e.control && e.keyCode == KeyCode.Q)
        {
            Debug.Log("Getting Closer");
            LoadScene();
        }
    }
}