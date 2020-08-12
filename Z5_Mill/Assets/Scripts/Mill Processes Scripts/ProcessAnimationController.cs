using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessAnimationController : MonoBehaviour
{
    [SerializeField] private List<AnimatorController> operations;
    [SerializeField] private float animSpeed;
    [SerializeField] private int index;

    private AnimatorController controller;

    // Start is called before the first frame update
    void Start()
    {
        if(index < 0 || index >= operations.Count)
        {
            index = 0;
        }
        foreach(AnimatorController animatorController in operations)
        {
            if(operations[index] == animatorController)
            {
                controller = animatorController;
            }
            else
            {
                animatorController.ActivateAnimator(false);
            }
        }
        if (controller != null)
        {
            controller.ActivateAnimator(true);
            controller.setAnimSpeed(animSpeed);
        }
    }

    public void StartCollsion()
    {
        if (controller != null)
        {
            controller.setAnimSpeed(0);
        }
    }

    public void EndCollision()
    {
        if (controller != null)
        {
            controller.setAnimSpeed(animSpeed);
        }
    }

    public void ChangeAnimator(int index)
    {
        if (!controller.inAction())
        {
            controller.ActivateAnimator(false);
            if (index < 0 || index >= operations.Count)
            {
                index = 0;
            }
            controller = operations[index];
            controller.ResetAnim();
        }

    }

}
