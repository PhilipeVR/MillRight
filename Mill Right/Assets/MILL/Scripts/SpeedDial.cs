using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode()]
public class SpeedDial : MonoBehaviour
{
    public int maximum;
    public int current;
    public Image mask;
    public Image fill;
    public Color color;

    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        float fillAmount = (float)current / (float)maximum;
        mask.fillAmount = fillAmount;

        fill.color = color;
    }
}
