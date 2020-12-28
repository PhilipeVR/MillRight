using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningTrigger : MonoBehaviour
{
    public DialogBox dialogues;
    public WarningManager warningManager;
    private List<Dialogue> gameDialogues;
    private int index;
    private Boolean awake = false;

    private void Awake()
    {
        gameDialogues = dialogues.dialogues;
        awake = true;

    }
    public void TriggerSentence(int dialogueIndex, int sentenceIndex)
    {
        if (!awake)
        {
            Awake();
        }
        warningManager.DisplaySentence(gameDialogues[dialogueIndex], sentenceIndex);
        index = dialogueIndex;
    }

}
