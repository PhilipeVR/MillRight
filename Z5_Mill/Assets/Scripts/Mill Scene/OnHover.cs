using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHover : MonoBehaviour
{
    // Start is called before the first frame update
    public Color hoverColor;
    private Color basicColor;
    private Renderer renderer;
    [SerializeField] private int detailIndex;
    [SerializeField] public GameObject ObjectManager;
    private ComponentManager manager;



    void Start()
    {
        renderer = GetComponent<Renderer>();
        basicColor = renderer.material.color;
        manager = ObjectManager.GetComponent<ComponentManager>();
    }

    public void setDetailIndex(int index)
    {
        detailIndex = index;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (transform.gameObject.Equals(hit.collider.gameObject))
                {
                    manager.SetDetails(detailIndex);
                }
            }
        }
    }

    // Update is called once per frame
    void OnMouseEnter()
    {
        renderer.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        renderer.material.color = basicColor;
    }
}
