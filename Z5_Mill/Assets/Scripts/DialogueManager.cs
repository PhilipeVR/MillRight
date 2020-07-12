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
    private Sprite[] images;
    private int count;
    private int index;

    private Queue<string> sentences;

    void Awake () {
        sentences = new Queue<string>();
        

        index = 1;
    }

    public void StartDialogue (Dialogue dialogue)
    {
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        count = sentences.Count;

        images = dialogue.images;

        DisplayNextSentence();
    }

    public void DisplayNextSentence ()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
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
}
