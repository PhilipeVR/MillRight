using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MeshTriggerController : MonoBehaviour
{
    [SerializeField] private List<TriggerMesh> triggerMeshes;
    public UnityEvent clickEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hovering(bool res)
    {
        foreach (Transform children in transform)
        {
            OnHover tmpHover = children.GetComponent<OnHover>();
            if (tmpHover != null)
            {
                if (res)
                {
                    tmpHover.SiblingHover();
                }
                else
                {
                    tmpHover.SiblingHoverExit();
                }
            }
        }
    }


}
