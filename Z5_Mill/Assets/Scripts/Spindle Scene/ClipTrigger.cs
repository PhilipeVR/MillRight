using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipTrigger : MonoBehaviour
{
    [SerializeField] private HintFlash flash;
    [SerializeField] private DialogueManager manager;
    [SerializeField] private string transitionParameter;
    [SerializeField] private string animationName;
    [SerializeField] private int index;
    [SerializeField] private int sentenceIndex;
    [SerializeField] private AnimationController controller;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private Button continueBTN;

    public Boolean triggerClicked = false;
    public Color[] basicColor;


    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        AnimatorEvents.current.animationDone += TriggerReset;
        if (GetComponent<Renderer>().materials.Length > 1)
        {
            basicColor = new Color[GetComponent<Renderer>().materials.Length];
            foreach (Material material in GetComponent<Renderer>().materials)
            {
                basicColor[index] = material.color;
                index++;
            }
        }
        else
        {
            basicColor = new Color[1];
            basicColor[index] = GetComponent<Renderer>().material.color;
        }
    }

    private void LateUpdate()
    {
        if (manager.SentenceIndex == sentenceIndex)
        {
            if (!triggerClicked)
            {
                continueBTN.interactable = false;
            }
            else
            {
                continueBTN.interactable = true;
            }
        }

    }

    private void PlaySequence()
    {
        controller.PlayAnimation(transitionParameter, animationName);
        flash.StopRoutine();
        continueBTN.interactable = true;
        triggerClicked = true;

    }

    private void OnMouseOver()
    {
        if (manager.SentenceIndex == sentenceIndex && index == controller.Index)
        {
            ChangeColor(hoverColor);
        }
    }

    private void OnMouseDown()
    {
        if (manager.SentenceIndex == sentenceIndex && index == controller.Index && controller.CurrentAnimationStatus)
        {
            ChangeColor(clickedColor);
            PlaySequence();
            manager.DisplayNextSentence();
        }
    }

    private void OnMouseExit()
    {
        BasicColor();
    }

    public void ChangeColor(Color color)
    {
        int index = 0;
        if (GetComponent<Renderer>().materials.Length > 1)
        {
            foreach (Material material in GetComponent<Renderer>().materials)
            {
                material.color = color;
                index++;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = color;
        }
    }
    public void BasicColor()
    {
        int index = 0;
        if (GetComponent<Renderer>().materials.Length > 1)
        {
            foreach (Material material in GetComponent<Renderer>().materials)
            {
                material.color = basicColor[index];
                index++;
            }
        }
        else
        {
            GetComponent<Renderer>().material.color = basicColor[index];
        }

    }

    public Boolean TriggerClicked
    {
        get => triggerClicked;
        set => triggerClicked = value;
    }

    public int SentenceIndex
    {
        get => sentenceIndex;
    }

    private void TriggerReset()
    {
        TriggerClicked = false;
    }
}
