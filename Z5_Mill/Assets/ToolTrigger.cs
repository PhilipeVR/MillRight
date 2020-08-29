using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnToolClickedTrigger()
    {
        WarningEvents.current.ToolClicked();
    }
}
