using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClipTriggers : MonoBehaviour
{
    [SerializeField] private Triggers animTriggers;
    [SerializeField] private TriggerAnimationController[] triggerController;
    [SerializeField] private ProcessAnimationController manager;
    private List<Trigger> triggers;

    // Start is called before the first frame update
    void Awake() {
        triggers = animTriggers.triggers;
        foreach(Trigger mia in triggers)
        {
            mia.Index = 0;
        }
    }

    // Update is called once per frame
    public void PlaySequence()
    {
        Trigger tmpTrigger = null;
        TriggerAnimationController controller = triggerController[manager.Index];
        foreach (Trigger trigger in triggers)
        {
            if(trigger.Anim == manager.Index && trigger.Name == controller.name)
            {
                tmpTrigger = trigger;
                break;
            }
        }
        if(tmpTrigger != null)
        {
            tmpTrigger.PlaySequence(controller);
        }
    }

    private void OnMouseDown()
    {
        PlaySequence();
    }


}
