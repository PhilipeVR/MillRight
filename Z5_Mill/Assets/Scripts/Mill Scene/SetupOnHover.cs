using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupOnHover : MonoBehaviour
{

    [SerializeField] private Color onHoverColor;
    [SerializeField] private int detailIndex;
    [SerializeField] GameObject ObjectManager;

    void Start()
    {
        foreach(Transform children in transform)
        {
            if (children.gameObject.GetComponent<MeshFilter>() != null)
            {
                setupMesh(children);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void setupMesh(Transform child)
    {
        MeshCollider collider = child.GetComponent<MeshCollider>();

        if (collider==null)
        {
            collider = child.gameObject.AddComponent<MeshCollider>();
        }

        collider.convex = true;
        collider.isTrigger = true;

        Renderer renderer = child.GetComponent<Renderer>();

        if (renderer == null)
        {
            renderer = child.gameObject.AddComponent<Renderer>();
        }

        OnHover hover = child.GetComponent<OnHover>();

        if (hover == null)
        {
            hover = child.gameObject.AddComponent<OnHover>();
        }

        hover.hoverColor = onHoverColor;
        hover.setDetailIndex(detailIndex);
        hover.ObjectManager = ObjectManager;

    }
}
