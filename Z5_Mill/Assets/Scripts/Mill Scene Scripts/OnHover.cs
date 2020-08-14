using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public Color hoverColor;
    public Color onClickedColor;
    private Color basicColor;
    private Boolean hovering = false;
    [SerializeField] private int detailIndex;
    [SerializeField] public GameObject ObjectManager;

    public Boolean hasBeenClicked = false;

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
        if (!hasBeenClicked)
        {
            if (!hovering)
            {
                GetComponent<Renderer>().material.color = hoverColor;
            }
        }
    }

    public void SiblingHoverExit()
    {
        if (!hasBeenClicked) 
        {
            if (basicColor == null)
            {
                setBasicColor();
            }
            GetComponent<Renderer>().material.color = basicColor;
        }
    }

    private void setBasicColor()
    {
        basicColor = GetComponent<Renderer>().material.color;
    }

    public void SetClickedColor()
    {
        GetComponent<Renderer>().material.color = Color.Lerp(basicColor, Color.black, 0.7f);
        //GetComponent<Renderer>().material.color = onClickedColor;
        //GetComponent<Renderer>().material.color = hoverColor;
        hasBeenClicked = true;
    }

    public IEnumerator FlashMesh(float interval)
    {
        if (!hasBeenClicked)
        {
            GetComponent<Renderer>().material.color = hoverColor;
            yield return new WaitForSecondsRealtime(interval);
            GetComponent<Renderer>().material.color = basicColor;
        }
    }
}
