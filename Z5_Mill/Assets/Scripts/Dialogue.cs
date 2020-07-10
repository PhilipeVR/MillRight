﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // To allow class to show up in the inspector to edit it
public class Dialogue {
    
    public string name;

    [TextArea(3, 10)]
    public string[] sentences;
}
