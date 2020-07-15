using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] public Button ON, MainMenu, Settings, FaceMill, SideMill, Drill, DRO_BTN;
    [SerializeField] public GameObject DRO_Panel;
    [SerializeField] public string intro, drilling, sidemilling, facemilling;
    [SerializeField] public DRO_Panel_Interactable_Manager DRO_manager;

    // Start is called before the first frame update
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
            ON.interactable = true;
            DRO_BTN.interactable = true;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            ON.interactable = true;
            DRO_BTN.interactable = true;
            DRO_Panel.GetComponent<DRO_ButtonHandler>().resetDRO();
            //setupSideMilling;
        }
        else if (dialogueName.Equals(facemilling))
        {
            ON.interactable = true;
            DRO_BTN.interactable = true;
        }

        DRO_manager.SetupDRO(dialogueName);
    }

    public void Transition(string dialogueName)
    {
        NoInteraction();
        if (dialogueName.Equals(intro))
        {
            Drill.interactable = true;
        }

        else if (dialogueName.Equals(drilling))
        {
            FaceMill.interactable = true;
        }
        else if (dialogueName.Equals(sidemilling))
        {
            Drill.interactable = true;
            FaceMill.interactable = true;
            SideMill.interactable = true;
            MainMenu.interactable = true;
        }
        else if (dialogueName.Equals(facemilling))
        {
            SideMill.interactable = true;

        }

    } 

    void NoInteraction()
    {
        DRO_BTN.interactable = false;
        ON.interactable = false;
        MainMenu.interactable = false;
        Settings.interactable = false;
        FaceMill.interactable = false;
        SideMill.interactable = false;
        Drill.interactable = false;
        DRO_Panel.SetActive(false);
    }
}
