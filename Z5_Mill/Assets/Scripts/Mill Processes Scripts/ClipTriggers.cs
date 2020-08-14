using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTriggers : MonoBehaviour
{
    [SerializeField] private int[] indexes;
    [SerializeField] private int[] anim;
    [SerializeField] private string[] transitionParameter;
    [SerializeField] private string[] animationName;
    [SerializeField] private int[] index;
    [SerializeField] private TriggerAnimationController[] trigger;
    [SerializeField] private ProcessAnimationController manager;
    private List<ClipTrigger> clipTriggers;

    // Start is called before the first frame update
    void Awake() {
        clipTriggers = new List<ClipTrigger>();
        for (int i = 0; i < trigger.Length; i++)
        {
            int last;
            if(i + 1 == trigger.Length)
            {
                last = transitionParameter.Length;
            }
            else
            {
                last = indexes[i + 1];
            }


            string[] tranParam = slice(transitionParameter, indexes[i], last);
            string[] animName = slice(animationName, indexes[i], last);
            int[] indexTrig = sliceInt(index, indexes[i], last);
            clipTriggers.Add(new ClipTrigger(manager, trigger[i], tranParam, animName, anim[i], indexTrig));
        }
    }

    public string[] slice(string[] val, int a, int b)
    {
        int d = 0;
        string[] newVal = new string[b - a];
        for(int i = a; i<b; i++)
        {
            newVal[d] = val[i];
            d++;
        }

        return newVal;
    }

    public int[] sliceInt(int[] val, int a, int b)
    {
        int d = 0;
        int[] newVal = new int[b - a];
        for (int i = a; i < b; i++)
        {
            newVal[d] = val[i];
            d++;
        }

        return newVal;
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        ClipTrigger tmpTrigger = null;
        foreach (ClipTrigger trigger in clipTriggers)
        {
            if(trigger.Anim == manager.Index)
            {
                tmpTrigger = trigger;
                break;
            }
        }
        if(tmpTrigger != null)
        {
            Debug.Log("Clip Triggers: " + tmpTrigger.Anim);
            tmpTrigger.PlaySequence();
        }
    }

    private void OnMouseDown()
    {
        PlaySequence();
    }
}
