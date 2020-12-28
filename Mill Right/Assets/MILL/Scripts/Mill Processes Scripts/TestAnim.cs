using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnim : MonoBehaviour
{
    [SerializeField] private string boolVal;
    [SerializeField] private string transition;
    [SerializeField] private TriggerAnimationController controller;
    // Start is called before the first frame update
    public void TestAnimation()
    {
        controller.PlayAnimation(boolVal, transition);
    }
}
