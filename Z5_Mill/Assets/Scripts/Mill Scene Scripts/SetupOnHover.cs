using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupOnHover : MonoBehaviour
{

    [SerializeField] private Color onHoverColor;
    [SerializeField] private int detailIndex;
    [SerializeField] GameObject ObjectManager;
    private Boolean clicked = false;

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

    // Update is called once per frame
    void Update()
    {
        
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

    }

    public void hasBeenClicked()
    {
        if (!clicked)
        {
            ObjectManager.GetComponent<ComponentManager>().incrementPartCounter();
            clicked = true;
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
}
