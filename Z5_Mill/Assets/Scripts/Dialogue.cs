using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] // To allow class to show up in the inspector to edit it
public class Dialogue {
    
    public string name;
    public string frenchName;

    [TextArea(3, 10)]
    public string[] sentences;    
    
    [TextArea(3, 10)]
    public string[] sentencesFR;

    public Sprite[] images;
}
