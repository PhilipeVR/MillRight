using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleText : MonoBehaviour
{
    // Start is called before the first frame update
    private string initialText;
    public string toggleOptionText;
    public Text currentText;
    void Awake()
    {
        initialText = currentText.text ;

    }

    // Update is called once per frame
    public void toggle()
    {

        if (currentText.text.Equals(initialText))
        {
            currentText.text = toggleOptionText;
        } else
        {
            currentText.text = initialText;

        }
    }
}
