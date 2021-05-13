using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerMeshTrigger : MonoBehaviour
{
    [SerializeField] private Toggle_On_Off powerController;
    [SerializeField] private bool OnOFF;
    [SerializeField] private Color HoverColor;
    private Color defaultColor;
    private MeshRenderer meshRenderer;
    private void Awake()
    {

        meshRenderer = GetComponent<MeshRenderer>();
        defaultColor = meshRenderer.material.color;
    }

    private void OnMouseDown()
    {
        if ( (OnOFF && !powerController.PowerState) || (!OnOFF && powerController.PowerState) )
        {
            powerController.OnOffToggle();
        }
    }

    private void OnMouseEnter()
    {
        meshRenderer.material.color = HoverColor;
    }

    private void OnMouseExit()
    {
        meshRenderer.material.color = defaultColor;
    }
}
