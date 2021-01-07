using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimRampDown : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string rotationParamName;
    [SerializeField] private float speedDecremmentRate;
    private float speed;
    float timer = 0;
    float timeToWait = 0.1f;
    bool checkingTime;
    bool timerDone;
    bool Cooldown = false;

    public void RampDown()
    {
        Debug.Log("Ramping Attempt");
        timer = 0;
        timeToWait = 0.1f;
        timerDone = false;
        speed = animator.GetFloat(rotationParamName);
        Cooldown = true;
    }

    private void LateUpdate()
    {
        if (Cooldown)
        {
            if (timerDone)
            {
                speed -= speedDecremmentRate;
                animator.SetFloat(rotationParamName, speed);
                timerDone = false;
            }
            else if (timer >= timeToWait)
            {
                timerDone = true;
                timer = 0;
            }

            if (speed < 0)
            {
                Cooldown = false;
                animator.speed = 0;
            }

            timer += Time.deltaTime;
        }
    }



}
