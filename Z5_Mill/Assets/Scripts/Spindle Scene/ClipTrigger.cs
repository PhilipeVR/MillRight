using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTrigger : MonoBehaviour
{
    [SerializeField] private string transitionParameter;
    [SerializeField] private string animationName;
    [SerializeField] private int index;
    [SerializeField] private AnimationController controller;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;

    public Color[] basicColor;


    // Start is called before the first frame update
    void Start()
    {
        int index = 0;
        Debug.Log(name + ": " + GetComponent<Renderer>().materials.Length);
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

    private void PlaySequence()
    {
        controller.PlayAnimation(transitionParameter, animationName);
    }

    private void OnMouseOver()
    {
        if (index == controller.Index)
        {
            ChangeColor(hoverColor);
        }
    }

    private void OnMouseDown()
    {
        if(index == controller.Index)
        {
            ChangeColor(clickedColor);
            PlaySequence();
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
        if(GetComponent<Renderer>().materials.Length > 1)
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
}
