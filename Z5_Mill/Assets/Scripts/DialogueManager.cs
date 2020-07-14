using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject DeactivateAtEndDialogue;
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

    void Awake () {
        sentences = new Queue<string>();
        sentencesFR = new Queue<string>();
        index = 1;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        titleLang = dialogue.frenchName;
        sentences.Clear();

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

        Debug.Log("End of conversation.");
        DeactivateAtEndDialogue.SetActive(false);
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
