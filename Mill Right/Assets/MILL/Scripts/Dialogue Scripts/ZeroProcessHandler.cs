using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZeroProcessHandler : MonoBehaviour
{
    [SerializeField] private DialogueManager manager;
    [SerializeField] private DRO_DisplayHandler displayHandler;
    [SerializeField] private ProcessAnimationController controller;
    [SerializeField] private Button ZeroButton, AxisButton;
    [SerializeField] private int animIndex;
    [SerializeField] private int sentenceIndex;
    [SerializeField] private int clipIndex;
    [SerializeField] private string axis;
    [SerializeField] private Color color;
    [SerializeField] private string transitionParams, animationClip;

    private Boolean Clicked = false;
    void Start()
    {
        ZeroButton.interactable = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(animIndex == controller.Index && sentenceIndex == manager.sentenceIndex && clipIndex == controller.Operation.TriggerAnimation.Index)
        {
            ZeroButton.interactable = true;
        }
    }

    public void AxisButtonSelected()
    {
        if (animIndex == controller.Index && sentenceIndex == manager.sentenceIndex)
        {
            AxisButton.image.color = color;
            Clicked = true;
        }
    }

    public void SetReferencePoint()
    {
        if (Clicked)
        {
            if (animIndex == controller.Index && sentenceIndex == manager.sentenceIndex && controller.Operation.TriggerAnimation.PlayAnimation(transitionParams, animationClip))
            {
                displayHandler.zero(axis);
                AxisButton.image.color = Color.white;
                ZeroButton.interactable = false;
                Clicked = false;
                manager.DisplayNextSentence();
            }
        }
    }
}
