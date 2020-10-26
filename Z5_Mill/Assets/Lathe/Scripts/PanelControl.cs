using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Panel1;
    [SerializeField] private GameObject Panel2;
    public bool isActive1 = false;
    public bool isActive2 = false;

    public void togglePanel1()
    {
        if (isActive1)
        {
            Panel1.SetActive(false);
        }
        else
        {
            Panel1.SetActive(true);
            if (isActive2)
            {
                Panel2.SetActive(false);
                isActive2 = !isActive2;
            }
        }
        isActive1 = !isActive1;
    }

    public void togglePanel2()
    {
        if (isActive2)
        {
            Panel2.SetActive(false);
        }
        else
        {
            Panel2.SetActive(true);
            if (isActive1)
            {
                Panel1.SetActive(false);
                isActive1 = !isActive1;
            }
        }
        isActive2 = !isActive2;
    }
}
