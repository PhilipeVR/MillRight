using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampDownSpeedMill : MonoBehaviour
{
    [SerializeField] private ProcessAnimationController operation;
    [SerializeField] private List<Animator> animators;
    [SerializeField] private string rotationParamName;
    [SerializeField] private float speedDecremmentRate;
    private float speed;
    float timer = 0;
    float timeToWait = 0.1f;
    bool checkingTime;
    bool timerDone;
    bool Cooldown = false;

    private Animator currentAnimator;


    public void RampDown()
    {
        Debug.Log("Ramping Attempt");
        timer = 0;
        timeToWait = 0.1f;
        timerDone = false;
        if (operation != null)
        {
            currentAnimator = animators[operation.Index];
        }
        else
        {
            currentAnimator = animators[0];
        }
        speed = currentAnimator.GetFloat(rotationParamName);
        Cooldown = true;
    }

    private void LateUpdate()
    {
        if (Cooldown)
        {
            if (timerDone)
            {
                speed -= speedDecremmentRate;
                currentAnimator.SetFloat(rotationParamName, speed);
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
            }

            timer += Time.deltaTime;
        }
    }



}
