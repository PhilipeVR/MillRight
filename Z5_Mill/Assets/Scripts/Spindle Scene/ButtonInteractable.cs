using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{
    [SerializeField] private int finalNum;
    [SerializeField] private Button button;
    [SerializeField] private AnimationController controller;
    private List<int> countChecker;
    private int counter;

    private void Start()
    {
        button.interactable = false;
        countChecker = new List<int>();
    }

    public void incrementCounter(int val) {
        if (!countChecker.Contains(val))
        {
            counter++;
        }
        InteractButton();
    }

    public void InteractButton()
    {
        if (counter == finalNum && controller.Counter > 0)
        {
            button.interactable = true;
        }
    }

}
