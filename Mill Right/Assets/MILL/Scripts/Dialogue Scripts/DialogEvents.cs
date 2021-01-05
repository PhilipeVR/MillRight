using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogEvents : MonoBehaviour
{
    public static DialogEvents current;
    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    // Update is called once per frame
    public event Action dialogFinished;
    public void DialogFinished()
    {
        if (dialogFinished != null)
        {
            dialogFinished();
        }
    }
}
