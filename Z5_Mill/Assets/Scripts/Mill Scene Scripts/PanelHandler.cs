using System;
using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject descriptionPanel, checklistPanel;
    private Boolean prevState = true;
    private Boolean visible = true;
    void Start()
    {
        activate(true);

    }

    // Update is called once per frame
    public void OnComponentClicked()
    {
        activate(true);
    }

    public void activateCheckList()
    {
        activate(false);

    }

    public void Visibility()
    {
        if (visible)
        {
            visible = false;
            descriptionPanel.SetActive(false);
            checklistPanel.SetActive(false);
        }
        else
        {
            visible = true;
            activate(prevState);
        }
    }

    private void activate(Boolean val)
    {
        if (visible)
        {
            if (val)
            {
                descriptionPanel.SetActive(true);
                checklistPanel.SetActive(false);
            }
            else
            {
                descriptionPanel.SetActive(false);
                checklistPanel.SetActive(true);
            }
        }
        prevState = val;
    }
}
