using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "DRO_Manager", menuName = "__UIPanel__/DRO_Manager")]
public class DRO_Manager : ScriptableObject
{

    private void Start()
    {
        // Must follow same order in hierarchy tree, depth-first search
        //inch = GetComponent<DRO_ButtonState>();
        //mm = GetComponent<DRO_ButtonState>();
    }

    public void UpdateDROManager(DRO_ButtonState buttonState)
    {
        Debug.Log("inch: "+(buttonState.buttonName == "inchButton"));
        Debug.Log("mm: "+(buttonState.buttonName == "mmButton"));
        Debug.Log("\n");

        if(buttonState.buttonName == "inchButton")
        {

        }
    }

    // Reset variables to default state
    // Call this method each time the game is started 
    // This is necessary since a ScriptableObject will remember the state of the variable when a game is finished
    public void ResetInput()
    {
        //m_toggleClicked = false;
    }


}
