using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacePieceTrigger : MonoBehaviour
{
    [SerializeField] private PlacePiece placePiece;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private GameObject activateOnClick;
    [SerializeField] private ZWheelControl WheelControl;
    [SerializeField] private QuillFeedControl QuillControl;
    [SerializeField] private FineAdjustmentControl fineAdjustmentControl;
    [SerializeField] private SwitchBit checkBitState;
    [SerializeField] private Button operate;
    private Color basicColor;
    private Boolean Clicked;


    // Start is called before the first frame update
    public void Start()
    {
        basicColor = GetComponent<Renderer>().material.color;
        Reset();
    }

    // Update is called once per frame
    private void PlaySequence()
    {
        placePiece.PlaceStock();
        Clicked = true;
    }

    private void OnMouseDown()
    {


        if (!Clicked)
        {

            if ((WheelControl.animTime > 0 || (QuillControl.Height < QuillControl.MaxHeight)) && checkBitState.CheckState())
            {

                WarningEvents.current.CutterNear();
            }
            else
            {
                GetComponent<Renderer>().material.color = clickedColor;
                activateOnClick.SetActive(true);
                gameObject.SetActive(false);
                operate.interactable = false;
                PlaySequence();
            }

        }

    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = basicColor;
    }

    private void OnMouseOver()
    {
        if (!Clicked)
        {
            GetComponent<Renderer>().material.color = hoverColor;
        }
    }

    public void Reset()
    {
        GetComponent<Renderer>().material.color = basicColor;
        gameObject.SetActive(true);
        activateOnClick.SetActive(false);
        Clicked = false;
    }

    public void Warning()
    {

    }
}
