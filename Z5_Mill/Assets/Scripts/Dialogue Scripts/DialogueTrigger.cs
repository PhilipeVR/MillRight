using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogBox dialogues;
    public InteractableManager manager;
    public DialogueManager dialogueManager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;
    private Boolean managerPresent;
    public Boolean sentenceTrigger;
    private void Awake()
    {
        Debug.Log("NOT HERE");
        gameDialogues = dialogues.dialogues;
    }

    void Start(){
        managerPresent = manager != null;
        if (!sentenceTrigger)
        {
            TriggerDialogue(0);
        }

    }

    public void TriggerDialogue (int i)
    {
        if (managerPresent)
        {
            manager.SetInteractionLevel(gameDialogues[i].name);
        }
        dialogueManager.StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

    public void TransitionDialogue() {
        if (managerPresent)
        {
            manager.Transition(gameDialogues[currentIndex].name);
        }
    }

    public void InteractButton()
    {
        if (managerPresent)
        {
            manager.InteractButton(gameDialogues[currentIndex].name);
        }
    }

}
