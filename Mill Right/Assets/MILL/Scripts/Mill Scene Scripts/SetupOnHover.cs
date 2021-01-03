using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SetupOnHover : MonoBehaviour
{
    [SerializeField] private Color onHoverColor;
    [SerializeField] private int detailIndex;
    [SerializeField] GameObject ObjectManager;
    [SerializeField] private Color onClickedColor;
    [SerializeField] private float FlashDelay = 0.25f;
    [SerializeField] private int NumOfFlash = 5;
    [SerializeField] private float timer = 0.1f;
    public Boolean clicked = false;
    public Boolean flashing = false;
    public UnityEvent clickEvent;

    public void hasBeenClicked()
    {
        StopAllCoroutines();
        if (!clicked)
        {
            clickEvent.Invoke();
            clicked = true;
            foreach(Transform children in transform)
            {
                OnHover tmpHover = children.GetComponent<OnHover>();
                if(tmpHover != null)
                {
                    tmpHover.SetClickedColor();
                }
            }
        }
    }

    public void Hovering(Boolean res)
    {
        foreach (Transform children in transform)
        {
            OnHover tmpHover = children.GetComponent<OnHover>();
            if (tmpHover != null) {
                if (res)
                {
                    tmpHover.SiblingHover();
                }
                else
                {
                    tmpHover.SiblingHoverExit();
                }
            }
        }
    }

    public void HintFlash()
    {
        flashing = true;
        foreach (Transform children in transform)
        {
            OnHover tmpHover = children.GetComponent<OnHover>();
            if (tmpHover != null)
            {
                StartCoroutine(tmpHover.FlashMesh(FlashDelay, NumOfFlash));
            }
        }
        flashing = false;
    }
}
