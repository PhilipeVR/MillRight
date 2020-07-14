using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start(){
        if(dialogue.name == "InitialDialogue")
        Debug.Log(dialogue.name == "InitialDialogue");
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue ()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    
}
