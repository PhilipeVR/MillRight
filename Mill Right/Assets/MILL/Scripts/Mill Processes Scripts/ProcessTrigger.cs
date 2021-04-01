using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProcessTrigger : MonoBehaviour
{
    [SerializeField] private DROToggle toggle;
    [DrawIf("PowerOrClip", false)] [SerializeField] private ClipTriggers clipTriggers;
    [DrawIf("PowerOrClip", true)] [SerializeField] private AnimPowerTrigger power;
    [DrawIf("ZeroProcessHandlerHere", true)] [SerializeField] private ZeroProcessHandler zeroProcessHandler;
    [SerializeField] private Color hoverColor, clickedColor;
    private Color normalColor;
    [SerializeField] private bool PowerOrClip = false;
    [SerializeField] private bool ZeroProcessHandlerHere = false;
    public UnityEvent OnClick;

    void Start()
    {
        NormalColor = GetComponent<Renderer>().material.color;
    }
    private void OnMouseDown()
    {
        if (!toggle.isActive)
        {
            toggle.toggle();
        }
        if (CheckSequence())
        {
            GetComponent<Renderer>().material.color = ClickedColor;
            if (PowerOrClip)
            {
                power.PlaySequence();
            }
            else
            {
                clipTriggers.PlaySequence();
            }
        }
        if (ZeroProcessHandlerHere)
        {
            zeroProcessHandler.AxisButtonSelected();
        }
        OnClick.Invoke();
    }

    private void OnMouseOver()
    {
        bool tmpZeroHandler = false;
        if(ZeroProcessHandlerHere)
        {
            tmpZeroHandler = zeroProcessHandler.CheckIndex();
        }

        if (CheckSequence() || tmpZeroHandler)
        {
            GetComponent<Renderer>().material.color = HoverColor;
        }
    }
    public bool CheckSequence()
    {
        if (PowerOrClip)
        {
            return power.RightSequence();
        }
        else
        {
            return clipTriggers.RightSequence();
        }
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
    private Color ClickedColor
    {
        get => clickedColor;
        set => clickedColor = value;
    }
    private Color NormalColor
    {
        get => normalColor;
        set => normalColor = value;
    }
}
