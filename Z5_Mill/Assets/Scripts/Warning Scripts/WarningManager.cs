using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class WarningManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private WarningTrigger trigger;
    [SerializeField] private Text nameText;
    [SerializeField] private Text dialogueText;
    [SerializeField] private GameObject spriteImage;
    [SerializeField] private Sprite[] images;
    [SerializeField] private GameObject DialogHolder;
    public Boolean language = true;

    private int index;
    private List<string> sentences, sentencesFR, currentLangSentences;
    private string lastSentence;
    private string titleLang;

    public void SwitchLang()
    {
        if (language)
        {
            currentLangSentences = sentencesFR;
        }
        else
        {
            currentLangSentences = sentences;
        }

        string tmp = nameText.text;
        nameText.text = titleLang;
        titleLang = tmp;

        string tmp2 = dialogueText.text;
        dialogueText.text = lastSentence;
        lastSentence = tmp2;

        language = !language;
    }

    public void DisplaySentence(Dialogue dialogue, int sentenceIndex)
    {
        if (!dialogue.name.Equals(nameText.text))
        {
            sentences = dialogue.sentences.ToList<string>();
            sentencesFR = dialogue.sentencesFR.ToList<string>();

            if (language)
            {
                currentLangSentences = sentences;
                nameText.text = dialogue.name;
                titleLang = dialogue.frenchName;
                lastSentence = sentencesFR[index];
            }
            else
            {
                currentLangSentences = sentencesFR;
                nameText.text = dialogue.frenchName;
                titleLang = dialogue.name;
                lastSentence = sentences[index];
            }
            images = dialogue.images;
        }
        DialogHolder.SetActive(true);
        dialogueText.text = currentLangSentences[sentenceIndex];
        spriteImage.GetComponent<Image>().sprite = images[sentenceIndex];
        index = sentenceIndex;

    }
}
