using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public DialogBox dialogues;
    public InteractableManager manager;
    private List<Dialogue> gameDialogues;
    private int currentIndex;

    void Start(){
        gameDialogues = dialogues.dialogues;
        TriggerDialogue(0);
    }

    public void TriggerDialogue (int i)
    {
        manager.SetInteractionLevel(gameDialogues[i].name);
        FindObjectOfType<DialogueManager>().StartDialogue(gameDialogues[i]);
        currentIndex = i;
    }

    public void TransitionDialogue() {
        manager.Transition(gameDialogues[currentIndex].name);
    }

    
}
