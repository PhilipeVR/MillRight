using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class ComponentHint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button hintButton;
    public List<SetupOnHover> partsClicked;
    public List<SetupOnHover> parts;
    public SetupOnHover hintedPart;
    public int index;

    void Awake()
    {
        parts = new List<SetupOnHover>();
        foreach (Transform children in transform)
        {
            if (children.gameObject.GetComponent<SetupOnHover>() != null)
            {
                parts.Add(children.gameObject.GetComponent<SetupOnHover>());
            }
        }
        index = UnityEngine.Random.Range(0, parts.Count);
        hintedPart = parts[index];
    }

    // Update is called once per frame
    public void ShowHint()
    {
        if (parts.Count > 0)
        {
            if (hintedPart == null)
            {
                index = UnityEngine.Random.Range(0, parts.Count);
                hintedPart = parts[index];
            }
            if (hintedPart.clicked)
            {
                RemoveClickedHint(hintedPart);
                index = UnityEngine.Random.Range(0, parts.Count);
                hintedPart = parts[index];
            }
            if (!hintedPart.flashing)
            {
                hintedPart.HintFlash();
            }
        }
        if(parts.Count == 0)
        {
            hintButton.interactable = false;
        }
    }

    public void RemoveClickedHint(SetupOnHover hint)
    {
        parts.Remove(hint);
        if (!partsClicked.Contains(hint))
        {
            partsClicked.Add(hint);
        }
    }
}
