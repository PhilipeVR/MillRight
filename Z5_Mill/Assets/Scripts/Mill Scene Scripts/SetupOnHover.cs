using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetupOnHover : MonoBehaviour
{

    [SerializeField] private Color onHoverColor;
    [SerializeField] private int detailIndex;
    [SerializeField] GameObject ObjectManager;
    [SerializeField] private Toggle checkListElem;
    [SerializeField] private PanelHandler handler;
    [SerializeField] private Color onClickedColor;
    [SerializeField] private float FlashDelay = 0.25f;
    public Boolean clicked = false;
    public Boolean flashing = false;

    void Start()
    {
        foreach(Transform children in transform)
        {
            if (children.gameObject.GetComponent<MeshFilter>() != null)
            {
                setupMesh(children);
            }
        }
    }

    private void setupMesh(Transform child)
    {
        BoxCollider collider = child.GetComponent<BoxCollider>();

        if (collider==null)
        {
            collider = child.gameObject.AddComponent<BoxCollider>();
        }

        collider.isTrigger = true;

        Renderer renderer = child.GetComponent<Renderer>();

        if (renderer == null)
        {
            renderer = child.gameObject.AddComponent<Renderer>();
        }

        OnHover hover = child.GetComponent<OnHover>();

        if (hover == null)
        {
            hover = child.gameObject.AddComponent<OnHover>();
        }

        hover.hoverColor = onHoverColor;
        hover.setDetailIndex(detailIndex);
        hover.ObjectManager = ObjectManager;
        hover.onClickedColor = onClickedColor;

    }

    public void hasBeenClicked()
    {
        StopAllCoroutines();
        if (!clicked)
        {
            ObjectManager.GetComponent<ComponentManager>().incrementPartCounter();
            handler.OnComponentClicked();
            checkListElem.isOn = true;
            clicked = true;
            foreach(Transform children in transform)
            {
                OnHover tmpHover = children.GetComponent<OnHover>();
                if(tmpHover != null)
                {
                    tmpHover.SetClickedColor();
                }
            }
            ComponentHint hintToRemove = GetComponentInParent<ComponentHint>();
            if(hintToRemove != null)
            {
                hintToRemove.RemoveClickedHint(this);
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
                Debug.Log("OnHover Flash Mesh Called");
                StartCoroutine(tmpHover.FlashMesh(FlashDelay));
            }
        }
        flashing = false;
    }
}
