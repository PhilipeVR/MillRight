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

    void Awake()
    {
        NoInteraction();
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
        }

        else if (dialogueName.Equals(drilling))
        {
            SideMill.interactable = true;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            FaceMill.interactable = true;
        }
        else if (dialogueName.Equals(facemilling))
        {
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
