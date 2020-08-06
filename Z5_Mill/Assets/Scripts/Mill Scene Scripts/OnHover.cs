using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public Color hoverColor;
    private Color basicColor;
    private Boolean hovering = false;
    [SerializeField] private int detailIndex;
    [SerializeField] public GameObject ObjectManager;

    private Boolean hasBeenClicked = false;

    void Awake()
    {
        setBasicColor();
    }

    public void setDetailIndex(int index)
    {
        detailIndex = index;
    }

    private void OnMouseDown()
    {
        ObjectManager.GetComponent<ComponentManager>().SetDetails(detailIndex);
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            GetComponentInParent<SetupOnHover>().hasBeenClicked();
        }
    }

    // Update is called once per frame
    void OnMouseOver()
    {
        SiblingHover();
        hovering = true;
        GetComponentInParent<SetupOnHover>().Hovering(hovering);

    }

    void OnMouseExit()
    {
        SiblingHoverExit();
        hovering = false;
        GetComponentInParent<SetupOnHover>().Hovering(hovering);

    }

    public void SiblingHover()
    {
        if (!hovering)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    public void SiblingHoverExit()
    {
        if(basicColor == null)
        {
            setBasicColor();
        }
        GetComponent<Renderer>().material.color = basicColor;
    }

    private void setBasicColor()
    {
        basicColor = GetComponent<Renderer>().material.color;
    }
}
