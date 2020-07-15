using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DRO_Panel_Interactable_Manager : MonoBehaviour
{
    [SerializeField] public string intro, drilling, sidemilling, facemilling, drillTag, endMillTag;
    [SerializeField] public GameObject Vise1, Vise2, Vise3;
    [SerializeField] public SwitchBit bitSwitching;

    // Update is called once per frame
    public void SetupDRO(string dialogueName)
    {

        if (dialogueName.Equals(drilling))
        {

            Vise1.SetActive(true);
            Vise2.SetActive(false);
            Vise3.SetActive(false);
            bitSwitching.changeChuck(true);
            bitSwitching.Switch(drillTag);


        }
        else if (dialogueName.Equals(sidemilling))
        {
            Vise1.SetActive(false);
            Vise2.SetActive(true);
            Vise3.SetActive(false);
            bitSwitching.changeChuck(false);
            bitSwitching.Switch(endMillTag);


        }
        else if (dialogueName.Equals(facemilling))
        {

            Vise1.SetActive(false);
            Vise2.SetActive(false);
            Vise3.SetActive(true);
            bitSwitching.changeChuck(false);
            bitSwitching.Switch(endMillTag);

        }


    }

}

