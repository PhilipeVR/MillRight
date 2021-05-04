using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerMesh : MonoBehaviour
{
    [SerializeField] private Color hoverColor;
    private Color normalColor;
    public UnityEvent OnClick;
    private bool clickedState = false;

    // Start is called before the first frame update
    void Start()
    {
        NormalColor = GetComponent<Renderer>().material.color;

    }

    // Update is called once per frame

    private void OnMouseDown()
    {
        if (clickedState)
        {
            GetComponent<Renderer>().material.color = NormalColor;
        }
        OnClick.Invoke();
        clickedState = !clickedState;
    }

    private void OnMouseOver()
    {
        GetComponent<Renderer>().material.color = HoverColor;
    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = NormalColor;
    }


    private Color HoverColor
    {
        get => hoverColor;
        set => hoverColor = value;
    }
    private Color NormalColor
    {
        get => normalColor;
        set => normalColor = value;
    }
}
