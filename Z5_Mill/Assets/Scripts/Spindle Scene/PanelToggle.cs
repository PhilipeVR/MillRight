using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelToggle : MonoBehaviour
{

    [SerializeField] GameObject descriptionPanel, toolSetupExplanation;

    private GameObject currentPanel;
    private Boolean state = false;

    // Start is called before the first frame update
    void Start()
    {
        descriptionPanel.SetActive(false);
        toolSetupExplanation.SetActive(true);
        currentPanel = toolSetupExplanation;
    }

    // Update is called once per frame
    public void showDescription()
    {
        if (toolSetupExplanation.activeSelf)
        {
            toolSetupExplanation.SetActive(false);
        }
        descriptionPanel.SetActive(true);

    }
    public void showExplanation()
    {
        if (descriptionPanel.activeSelf)
        {
            descriptionPanel.SetActive(false);
        }
        toolSetupExplanation.SetActive(true);

    }

    public void toggle()
    {
        if (state)
        {
            if (currentPanel.Equals(toolSetupExplanation))
            {
                descriptionPanel.SetActive(false);
                toolSetupExplanation.SetActive(true);
            }
            else
            {
                toolSetupExplanation.SetActive(false);
                descriptionPanel.SetActive(true);

            }
        } else
        {
            if (toolSetupExplanation.activeSelf)
            {
                currentPanel = toolSetupExplanation;
            } else
            {
                currentPanel = descriptionPanel;
            }

            toolSetupExplanation.SetActive(state);
            descriptionPanel.SetActive(state);
        }

        state = !state;
    }


}
