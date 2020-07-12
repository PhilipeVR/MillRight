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
    void Start()
    {
        initialText = currentText.text ;
        Debug.LogWarning("initial");

    }

    // Update is called once per frame
    public void toggle()
    {
        Debug.LogWarning("Toggle");

        if (currentText.text.Equals(initialText))
        {
            currentText.text = toggleOptionText;
        } else
        {
            currentText.text = initialText;

        }
        Debug.LogWarning(currentText.text);

    }
}
