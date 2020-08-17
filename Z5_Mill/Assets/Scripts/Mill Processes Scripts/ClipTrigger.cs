using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ClipTrigger
{

    private int anim;
    private string[] transitionParameter;
    private string[] animationName;
    private int[] index;
    private int i = 0;
    private TriggerAnimationController trigger;
    private ProcessAnimationController manager;
    // Start is called before the first frame update
    public ClipTrigger(ProcessAnimationController manager, TriggerAnimationController trigger, string[] transitionParameter, string[] animationName, int anim, int[] index)
    {
        this.manager = manager;
        this.trigger = trigger;
        this.transitionParameter = transitionParameter;
        this.animationName = animationName;
        this.anim = anim;
        this.index = index;
        
        string e = "| ";
        foreach (string f in animationName)
        {
            e += f + " | ";
        }

        string c = "| ";
        foreach(string a in transitionParameter)
        {
            c += a + " | ";
        }
        string d = "| ";
        foreach (int b in index)
        {
            d += b.ToString() + " | ";
        }
        Debug.Log("ClipTrigger Transition Params " + anim.ToString() + ": "  + c);
        Debug.Log("ClipTrigger Indexes " + anim.ToString() + ": " + d);
        Debug.Log("ClipTrigger Animation Names " + anim.ToString() + ": " + e);

    }



    // Update is called once per frame
    public Boolean PlaySequence()
    {
        Boolean val = false;
        //Debug.Log("Clip Trigger: " + index[i] + " and " + i);

        if (i < transitionParameter.Length)
        {
            if (index[i] == trigger.Index)
            {
                //Debug.Log("Clip Trigger: " + animationName[i] + " and " + transitionParameter[i]);
                val = trigger.PlayAnimation(transitionParameter[i], animationName[i]);

                if (val)
                {
                    i++;
                }
            }
        }

        return val;

    }

    public int Anim
    {
        get => anim;
    }
}
