using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerButtonToggle
{
    private int anim;
    private string[] transitionParameter;
    private string[] animationName;
    private int[] index;
    private int[] sentenceIndex;
    private TriggerAnimationController trigger;
    private string OnText, OffText;
    private int i = 0;
    private Transform transform;
    private Image image;
    private ProcessAnimationController manager;
    private Boolean millOn = false;
    private int LateAnimIndex;

    // Start is called before the first frame update
    public PowerButtonToggle(Transform transform, Image image, ProcessAnimationController manager, string onText, string offText, TriggerAnimationController trigger, string[] transitionParameter, string[] animationName, int anim, int[] index, int[] sentenceIndex)
    {

        this.OnText = onText;
        this.OffText = offText;
        this.transform = transform;
        this.image = image;
        this.manager = manager;
        this.index = index;
        this.sentenceIndex = sentenceIndex;
        this.trigger = trigger;
        this.animationName = animationName;
        this.anim = anim;
        this.transitionParameter = transitionParameter;
        millOn = false;
        transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
        image.color = Color.green;

    }

    public Boolean PlaySequence()
    {
        Boolean val = false;
        if (i < 2)
        {
            if (index[i] == trigger.Index && CheckManager())
            {
                val = trigger.PlayAnimation(transitionParameter[i], animationName[i]);
                if (val)
                {
                    i++;
                }
            }
        }
        return val;

    }

    public void OnOffToggle(DialogueManager manager)
    {
        if (i < 2)
        {
            if (manager.SentenceIndex == SentenceIndex())
            {
                if (millOn)
                {
                    if (PlaySequence())
                    {
                        transform.GetChild(0).gameObject.GetComponent<Text>().text = OnText;
                        image.color = Color.green;
                        millOn = !millOn;
                        if (manager.sentenceIndex == CurrentSentenceIndex())
                        {
                            manager.DisplayNextSentence();
                        }
                    }

                }
                else
                {
                    if (PlaySequence())
                    {
                        transform.GetChild(0).gameObject.GetComponent<Text>().text = OffText;
                        image.color = Color.red;
                        millOn = !millOn;
                        if (manager.sentenceIndex == CurrentSentenceIndex())
                        {
                            manager.DisplayNextSentence();
                        }
                    }
                }
            }
        }
        
    }

    public Boolean getMillState()
    {
        return millOn;
    }

    private Boolean CheckManager()
    {
        return manager.Index == anim;
    }

    public int Anim
    {
        get => anim;
    }

    public int Index
    {
        get => i;
        set => i = value;
    }

    public int CurrentSentenceIndex()
    {
        int val = sentenceIndex[LateAnimIndex];
        LateAnimIndex++;
        return val;
    }

    public int SentenceIndex()
    {
        int val = -1;
        if(Index < sentenceIndex.Length)
        {
            val = sentenceIndex[Index];

        }
        else
        {
            val = sentenceIndex[sentenceIndex.Length -1];

        }
        return val;
    }
}
