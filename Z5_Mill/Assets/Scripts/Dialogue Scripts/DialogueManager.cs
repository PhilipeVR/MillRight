using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private ProcessAnimationController animationController;
    private int counter;
    public GameObject DeactivateAtEndDialogue;
    public DialogueTrigger trigger;
    public Text nameText;
    public Text dialogueText;
    public Text dialogIndex;
    public GameObject Image;
    public Boolean language = true;

    private Sprite[] images;
    private int count;
    private int index;
    private Queue<string> sentences; // Keeps track of all sentences in current dialogue
    private Queue<string> sentencesFR;
    private Queue<string> currentLangSentence;
    private string lastSentence;
    private string titleLang;
    private Boolean controllerPresent;

    void Awake () {
        controllerPresent = animationController != null;
        counter = 0;
        sentences = new Queue<string>();
        sentencesFR = new Queue<string>();
        index = 1;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        titleLang = dialogue.frenchName;
        sentences.Clear();
        index = 1;
        DeactivateAtEndDialogue.SetActive(true);

        if (language)
        {
            nameText.text = dialogue.name;
            titleLang = dialogue.frenchName;
        }
        else
        {
            nameText.text = dialogue.frenchName;
            titleLang = dialogue.name;
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        foreach(string sentece in dialogue.sentencesFR)
        {
            sentencesFR.Enqueue(sentece);
        }

        count = sentences.Count;

        images = dialogue.images;

        if (language)
        {
            currentLangSentence = sentences;
        }
        else
        {
            currentLangSentence = sentencesFR;
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = currentLangSentence.Dequeue();
        if (language)
        {
            lastSentence = sentencesFR.Dequeue();
        } else
        {
            lastSentence = sentences.Dequeue();
        }

        dialogueText.text = sentence;
        dialogIndex.text = index + "/" + count;
        Image.GetComponent<Image>().sprite = images[index - 1];
        index++;


    }

    void EndDialogue()
    {
        DeactivateAtEndDialogue.SetActive(false);
        if(controllerPresent)
        {
            if (!animationController.Operation.TriggerAnimation.Done && animationController.Operation.TriggerAnimation.Active)
            {
                trigger.InteractButton();
            }
            else if (animationController.Operation.TriggerAnimation.Done || (counter == 0 && !animationController.Operation.TriggerAnimation.Active))
            {
                trigger.TransitionDialogue();
                counter++;
            }

        }
    }

    public void SkipDialogue()
    {
        if (animationController.Operation.TriggerAnimation.Done && animationController.Operation.TriggerAnimation.Active)
        {
            DeactivateAtEndDialogue.SetActive(false);
            trigger.TransitionDialogue();
            counter++;
        }
    }

    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent(KeyCode.N.ToString())) && controllerPresent)
        {
            SkipDialogue();
        }
    }

    public void switchLang() {
        if (language)
        {
            currentLangSentence = sentencesFR;
        } else
        {
            currentLangSentence = sentences;
        }

        string tmp = nameText.text;
        nameText.text = titleLang;
        titleLang = tmp;

        string tmp2 = dialogueText.text;
        dialogueText.text = lastSentence;
        lastSentence = tmp2;

        language = !language;
     }
}
