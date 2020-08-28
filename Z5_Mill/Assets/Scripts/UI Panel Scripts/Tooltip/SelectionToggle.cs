using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionToggle : MonoBehaviour
{
    [SerializeField] private Color color;
    [SerializeField] private List<Image> images;
    private int pointer = -1;

    public void Clicked(string name)
    {
        if(pointer > 0)
        {
        }
        int index = 0;
        foreach(Image image in images)
        {
            Debug.Log("Image: " + image.name);

            if (image.name.Equals(name))
            {
                pointer = index;
                image.color = color;
            }
            else
            {
                image.color = Color.white;
            }
            index++;
        }
    }

    public void Clean()
    {
        foreach (Image image in images)
        {
            image.color = Color.white;
        }
    }
}
