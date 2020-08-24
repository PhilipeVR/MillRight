using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimClipTrigger : MonoBehaviour
{
    [SerializeField] private ProcessAnimationController manager;
    [SerializeField] private int anim;
    [SerializeField] private string transitionParameter;
    [SerializeField] private string animationName;
    [SerializeField] private int index;
    [SerializeField] private TriggerAnimationController trigger;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private Boolean colorChange;
    [SerializeField] private GameObject activateOnClick;

    public Color basicColor;

    public void Awake()
    {
        if (colorChange)
        {
            basicColor = GetComponent<Renderer>().material.color;
            if(activateOnClick != null)
            {
                activateOnClick.SetActive(false);
            }
        }
    }

    public void PlaySequence()
    {
       
        //Debug.Log(animationName);
        if(index == trigger.Index && CheckManager())
        {
            trigger.PlayAnimation(transitionParameter, animationName);
        }
        
    }

    private void OnMouseDown()
    {
        if (colorChange && CheckManager() && index == trigger.Index)
        {
            GetComponent<Renderer>().material.color = clickedColor;
            if(activateOnClick != null)
            {
                activateOnClick.SetActive(true);
                gameObject.SetActive(false);
            }
        }
        PlaySequence();
    }

    private void OnMouseOver()
    {
        //Debug.Log("AnimClipTrigger " + "(" + gameObject.name + ") : " + manager.Index);
        if (colorChange && CheckManager() && index == trigger.Index) {
            GetComponent<Renderer>().material.color = hoverColor;

        }
    }

    private void OnMouseExit()
    {
        if (colorChange && CheckManager())
        {
            GetComponent<Renderer>().material.color = basicColor;
        }
    }

    private Boolean CheckManager()
    {
        return manager.Index == anim;
    }

    public void Reset()
    {

        if (colorChange)
        {
            GetComponent<Renderer>().material.color = basicColor;
            if (activateOnClick != null)
            {
                gameObject.SetActive(true);
                activateOnClick.SetActive(false);
            }
        }
    }


}
