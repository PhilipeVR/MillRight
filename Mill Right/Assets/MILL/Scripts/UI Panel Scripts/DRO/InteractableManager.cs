using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] public Button ON, MainMenu,Next, FaceMill, SideMill, Drill, DRO_BTN;
    [SerializeField] public DROToggle DRO_Panel;
    [SerializeField] public string intro, drilling, sidemilling, facemilling;
    private Boolean SequenceDone;
    private anim currentAnim;
    void Awake()
    {
        //NoInteraction();
    }

    public anim CurrentAnim
    {
        get => currentAnim;
    }

    public void SetupAnims(anim savedAnim)
    {
        if (savedAnim == anim.Drilling)
        {
            Transition(intro);
        }
        else if (savedAnim == anim.SideMilling)
        {
            Transition(drilling);
        }
        else if (savedAnim == anim.Facing)
        {
            Transition(sidemilling);
        }
        else
        {
            Transition(facemilling);
        }
    }

    // Update is called once per frame
    public void SetInteractionLevel(string dialogueName)
    {
        NoInteraction();

        if (dialogueName.Equals(drilling))
        {
            DRO_BTN.interactable = true;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            DRO_BTN.interactable = true;
            //DRO_Panel.GetComponent<DRO_Manager>().resetDRO();
            //setupSideMilling;
        }
        else if (dialogueName.Equals(facemilling))
        {
            DRO_BTN.interactable = true;
        }
    }

    public void Transition(string dialogueName)
    {

        NoInteraction();
        if (SequenceDone)
        {
            Drill.interactable = true;
            FaceMill.interactable = true;
            SideMill.interactable = true;
            MainMenu.interactable = true;
            Next.interactable = true;
        }

        else if (dialogueName.Equals(intro))
        {
            Drill.interactable = true;
            currentAnim = anim.Drilling;
        }

        else if (dialogueName.Equals(drilling))
        {
            SideMill.interactable = true;
            currentAnim = anim.SideMilling;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            FaceMill.interactable = true;
            currentAnim = anim.Facing;
        }
        else if (dialogueName.Equals(facemilling))
        {
            currentAnim = anim.Complete;
            Drill.interactable = true;
            FaceMill.interactable = true;
            SideMill.interactable = true;
            MainMenu.interactable = true;
            Next.interactable = true;
            SequenceDone = true;

        }

    }

    public void InteractButton(string dialogueName)
    {
        if (dialogueName.Equals(drilling))
        {
            Drill.interactable = true;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            SideMill.interactable = true;
        }
        else if (dialogueName.Equals(facemilling))
        {
            FaceMill.interactable = true;
        }
    }
    void NoInteraction()
    {
        DRO_BTN.interactable = false;
        ON.interactable = false;
        Next.interactable = false;
        MainMenu.interactable = false;
        FaceMill.interactable = false;
        SideMill.interactable = false;
        Drill.interactable = false;
        DRO_Panel.activate(false);
    }
}

public enum anim { Drilling, SideMilling, Facing, Complete, NA }

