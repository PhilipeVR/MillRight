using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimPowerTrigger : MonoBehaviour
{
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private bool state;
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private TriggerAnimationController[] triggerController;
    [SerializeField] private ProcessAnimationController manager;
    [SerializeField] private Image stateImage;
    [SerializeField] private Text stateText;
    [DrawIf("state", false)] [SerializeField] private RampDownSpeedMill ramp;
    [DrawIf("state", false)] [SerializeField] private string OffText;
    [DrawIf("state", true)] [SerializeField] private string OnText;

    private List<Trigger> triggers;

    void Awake()
    {
        triggers = animTriggers.triggers;
        ResetTriggers(-1);
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        TriggerAnimationController controller = triggerController[manager.Index];
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
            bool val = tmpTrigger.PlaySequence(controller);
            if (val)
            {
                if (dialogueManager.SentenceIndex == tmpTrigger.CurrentSentenceIndex())
                {
                    dialogueManager.DisplayNextSentence();
                    if (state)
                    {
                        stateImage.color = Color.red;
                        stateText.text = OnText;
                    }
                    else
                    {
                        stateImage.color = Color.green;
                        stateText.text = OffText;
                        ramp.RampDown();
                    }
                }
            }
        }
    }

    public bool RightSequence()
    {
        TriggerAnimationController controller = triggerController[manager.Index];
        foreach (Trigger trigger in triggers)
        {
            if (trigger.Anim == manager.Index && trigger.Name == controller.name && trigger.SentenceIndex() == dialogueManager.SentenceIndex)
            {
                return true;
            }
        }
        return false;
    }

    private void OnMouseDown()
    {
        PlaySequence();
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
