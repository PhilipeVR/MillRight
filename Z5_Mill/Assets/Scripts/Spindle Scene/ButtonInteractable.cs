using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{
    [SerializeField] private int finalNum;
    [SerializeField] private Button button;
    [SerializeField] private AnimationController controller;
    [SerializeField] private VideoController videoController;
    [SerializeField] private DialogueManager manager;
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

    public void LateUpdate()
    {
        if(manager.SentenceIndex == manager.count - 1)
        {
            InteractButton();
        }
    }

    public void InteractButton()
    {
        if(counter == finalNum && controller.Counter > 0 && !videoController.PlayedOnce && manager.SentenceIndex == manager.count-1)
        {
            videoController.StartVideo();
            videoController.PlayVideo();
        }
        else if (counter == finalNum && controller.Counter > 0 && videoController.PlayedOnce && manager.SentenceIndex == manager.count - 1)
        {
            button.interactable = true;
        }
    }

}
