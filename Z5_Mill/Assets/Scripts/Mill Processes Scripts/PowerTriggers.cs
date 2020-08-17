using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerTriggers : MonoBehaviour
{
    [SerializeField] private int[] anim;
    [SerializeField] private string[] transitionParameter;
    [SerializeField] private string[] animationName;
    [SerializeField] private int[] index;
    [SerializeField] private TriggerAnimationController[] trigger;
    [SerializeField] private ProcessAnimationController manager;
    [SerializeField] private string OnText, OffText;
    private List<PowerButtonToggle> powers;

    // Start is called before the first frame update
    void Start()
    {
        powers = new List<PowerButtonToggle>();
        for (int i = 0; i < trigger.Length; i++)
        {
            int a = i * 2;
            int b = a + 1;
            powers.Add(new PowerButtonToggle(transform, GetComponent<Image>(), manager, OnText, OffText, trigger[i], transitionParameter, new string[] { animationName[a], animationName[b] }, anim[i], new int[] { index[a], index[b] }));
        }
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        powers[manager.Index].OnOffToggle();
        PowerButtonToggle buttonToggle = null;
        foreach (PowerButtonToggle toggle in powers)
        {
            if (toggle.Anim == manager.Index)
            {
                buttonToggle = toggle;
                break;
            }
        }
        if (buttonToggle != null)
        {
            buttonToggle.OnOffToggle();
        }
    }
}
