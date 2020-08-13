using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableManager : MonoBehaviour
{
    [SerializeField] public Button ON, MainMenu,Next, FaceMill, SideMill, Drill, DRO_BTN;
    [SerializeField] public GameObject DRO_Panel;
    [SerializeField] public string intro, drilling, sidemilling, facemilling;

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
    }

    public void Transition(string dialogueName)
    {

        if (ON.GetComponent<Toggle_On_Off>().getMillState())
        {
            ON.GetComponent<Toggle_On_Off>().OnOffToggle();
        }

        NoInteraction();

        if (dialogueName.Equals(intro))
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

        }

    } 

    void NoInteraction()
    {
        DRO_BTN.interactable = false;
        ON.interactable = false;
        MainMenu.interactable = false;
        FaceMill.interactable = false;
        SideMill.interactable = false;
        Drill.interactable = false;
        DRO_Panel.SetActive(false);
    }
}
