using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessController : MonoBehaviour
{
    //Need a reset stock material script one in the mill
    [SerializeField] private HintTrigger hintTriggerFlash;
    //[SerializeField] private ButtonReset reset;
    [SerializeField] private List<OperationManager> operations;
    [SerializeField] private RevertDestruction revertDestruction;
    [SerializeField] private float animSpeed;

    private int m_index;
    private OperationManager controller;

    void Awake()
    {
        controller = null;
        foreach (OperationManager operation in operations)
        {
            operation.ActivateAnimator(false);
        }
    }

    public void ChangeAnimator(int index)
    {
        bool prevState = false;
        OperationManager prevController = controller;
        if (controller == null || controller.Done)
        {
            if (controller != null)
            {
                prevState = controller.Done;
                controller.ActivateAnimator(false);
                revertDestruction.RevertStock();

            }
            if (index >= 0 && index < operations.Count)
            {
                controller = operations[index];
                controller.ResetAnim(prevState, prevController);
                m_index = index;
                hintTriggerFlash.SetAnimIndex();
                //reset.ResetDRO();
            }
        }
    }

    public void StartCollsion()
    {
        if (controller != null)
        {
            controller.SetAnimSpeed(0);
        }
    }

    public void EndCollision()
    {
        if (controller != null)
        {
            controller.SetAnimSpeed(animSpeed);
        }
    }

    public int Index
    {
        get => m_index;
    }

    public OperationManager Operation
    {
        get => controller;
    }


}
