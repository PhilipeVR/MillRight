﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataHandler
{

    private bool language;
    private string scene, student, studentNumber;
    private bool isTutorial;
    private List<int> componentsCompleted;
    private anim currentAnim;
    private bool[] completed;

    public DataHandler(bool lang, string student, string studentNumber, string scene, bool isTutorial, ComponentHint componentHint)
    {
        this.student = student;
        this.studentNumber = studentNumber;
        this.language = lang;
        this.scene = scene;
        this.componentsCompleted = new List<int>();
        this.isTutorial = isTutorial;
        foreach(SetupOnHover part in componentHint.partsClicked)
        {
            this.componentsCompleted.Add(part.DetailIndex);
        }
        this.currentAnim = anim.NA;

    }


    public DataHandler(bool lang, string student, string studentNumber, string scene, bool isTutorial, anim curr)
    {
        this.student = student;
        this.studentNumber = studentNumber;
        this.language = lang;
        this.scene = scene;
        this.isTutorial = isTutorial;
        this.currentAnim = curr;
        this.componentsCompleted = null;
        this.currentAnim = curr;
    }

    public DataHandler(bool lang, string student, string studentNumber, string scene, bool isTutorial, bool[] completedOperations)
    {
        this.student = student;
        this.studentNumber = studentNumber;
        this.language = lang;
        this.scene = scene;
        this.componentsCompleted = new List<int>();
        this.isTutorial = isTutorial;
        this.completed = completedOperations;
        this.currentAnim = anim.NA;

    }

    public bool[] Completed
    {
        get => completed;
    }

    public List<int> DetailIndexes
    {
        get => componentsCompleted;
    }

    public bool Language
    {
        get => language;
    }

    public string StudentName
    {
        get => student;
    }

    public string StudentNumber
    {
        get => studentNumber;
    }

    public string savedLevel
    {
        get => scene;
    }

    public bool IsTutorial
    {
        get => isTutorial;
    }

    public anim SavedAnim
    {
        get => currentAnim;
    }

}
