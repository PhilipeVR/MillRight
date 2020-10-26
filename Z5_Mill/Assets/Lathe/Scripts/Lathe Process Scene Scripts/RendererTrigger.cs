using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RendererTrigger : MonoBehaviour
{
    [SerializeField] private DialogueOperator dialogueManager;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private ProcessController manager;
    [SerializeField] private Color hoverColor;
    [SerializeField] private Color clickedColor;
    [SerializeField] private bool colorChange;
    [SerializeField] private GameObject activateOnClick;
    private List<Trigger> triggers;
    private Color basicColor;


    // Start is called before the first frame update
    void Awake()
    {
        if (colorChange)
        {
            basicColor = GetComponent<Renderer>().material.color;
            if (activateOnClick != null)
            {
                activateOnClick.SetActive(false);
            }
        }
        triggers = animTriggers.triggers;
        ResetTriggers(-1);
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        OperationManager controller = manager.Operation;
        foreach (Trigger trigger in triggers)
        {
            if (trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
            {
                tmpTrigger = trigger;
                break;
            }
        }
        if (tmpTrigger != null)
        {
            Debug.Log(tmpTrigger.TriggerControllerName);
            bool val = tmpTrigger.PlaySequence(controller);
            if (val)
            {
                if (colorChange)
                {
                    GetComponent<Renderer>().material.color = clickedColor;
                    if (activateOnClick != null)
                    {
                        activateOnClick.SetActive(true);
                        gameObject.SetActive(false);
                    }
                }
                if (dialogueManager.SentenceIndex == tmpTrigger.CurrentSentenceIndex())
                {
                    dialogueManager.DisplayNextSentence();
                }
            }
        }
    }

    private void OnMouseDown()
    {
        PlaySequence();
    }

    private void OnMouseOver()
    {
        OperationManager controller = manager.Operation;
        if (controller != null)
        {
            foreach (Trigger trigger in triggers)
            {
                if (trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
                {
                    GetComponent<Renderer>().material.color = hoverColor;
                    break;
                }
            }
        }

    }

    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = basicColor;
    }

    public void ResetTriggers(int anim)
    {
        foreach (Trigger trigger in triggers)
        {
            if (anim < 0)
            {
                trigger.Index = 0;
            }
            else if (trigger.Anim == anim)
            {
                trigger.Index = 0;
            }

        }
    }
}
