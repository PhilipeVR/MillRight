using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogBox dialogues;
    public InteractableManager manager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;
    private Boolean managerPresent;

    void Start(){
        managerPresent = manager != null;
        gameDialogues = dialogues.dialogues;
        TriggerDialogue(0);

    }

    public void TriggerDialogue (int i)
    {
        if (managerPresent)
        {
            manager.SetInteractionLevel(gameDialogues[i].name);
        }
        FindObjectOfType<DialogueManager>().StartDialogue(gameDialogues[i]);
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
