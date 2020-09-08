using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SumbitInformation : MonoBehaviour
{
    [SerializeField] private LanguageSceneToggle InputInformation;
    [SerializeField] private Text fullNameText;
    [SerializeField] private Text studentNumberText;

    public void SubmitInformation()
    {
        InputInformation.setName(fullNameText.text);
        InputInformation.setNumber(studentNumberText.text);
    }
}
