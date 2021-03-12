using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInteractable : MonoBehaviour
{
    [SerializeField] private int finalNum;
    [SerializeField] private Button button;
    [SerializeField] private AnimationController controller;
    [SerializeField] private VideoManager2 videoController;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private int VideoIndex;
    private List<int> countChecker;
    private int counter;
    private int dialogCounter;
    private bool Played;
   
    private void Start()
    {
        button.interactable = false;
        countChecker = new List<int>();
        DialogEvents.current.dialogFinished += DialogDone;
    }

    public void incrementCounter(int val) {
        if (!countChecker.Contains(val))
        {
            countChecker.Add(val);
            counter++;
        }
        InteractButton();
    }

    public void DialogDone()
    {
        if(dialogCounter == 0)
        {
            InteractButton();
        }
        dialogCounter++;
    }

    public void InteractButton()
    {
        if(counter == finalNum && controller.Counter > 0 && !Played && manager.SentenceIndex == manager.count)
        {
            videoController.PlayYoutubePlayer(VideoIndex);
        }
        else if (counter == finalNum && controller.Counter > 0 && Played && manager.SentenceIndex == manager.count)
        {
            button.interactable = true;
        }
    }

}
