﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private Sprite initialSprite;
    public Sprite toggleOptionSprite;
    public Image currentIMG;
    void Awake()
    {
        initialSprite = currentIMG.sprite;

    }

    // Update is called once per frame
    public void toggle()
    {

        if (currentIMG.sprite.Equals(initialSprite))
        {
            currentIMG.sprite = toggleOptionSprite;
        }
        else
        {
            currentIMG.sprite = initialSprite;

        }
    }
}
