using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarningListenener : MonoBehaviour
{
    [SerializeField] private Button QuizBTN;
    // Start is called before the first frame update
    void Start()
    {
        QuizBTN.interactable = false;
        WarningEvents.current.allCompleted += Interactable;
    }

    // Update is called once per frame
    private void Interactable()
    {
        QuizBTN.interactable = true;
    }
}
