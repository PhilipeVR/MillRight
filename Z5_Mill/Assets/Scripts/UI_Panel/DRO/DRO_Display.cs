
using UnityEngine;
using UnityEngine.UI;

public class DRO_Display : MonoBehaviour
{
    public Transform stockMaterial;
    public Text xText;
    public Text yText;
    public Text zText;

    public Button inch; //inch is the default unit
    public Button mm;

    private string activeUnits;

    void Update()
    {
        // Need to convert distance from "metres" to "inch" or "mm"
        // Also make this update only when there is a change in these coords

        checkUnits();
        xText.text = convertUnits(stockMaterial.position.x).ToString();
        yText.text = convertUnits(stockMaterial.position.y).ToString();
        zText.text = convertUnits(stockMaterial.position.z).ToString();
    }

    void checkUnits()
    {
        if (inch.isActiveAndEnabled)
        {
            activeUnits = "inch";
        }
        if(mm.isActiveAndEnabled)
        {
            activeUnits = "mm";
        }
    }

    private float convertUnits(float num)
    {
        if (activeUnits == "mm")
        {
            return (num * 1000f); // 1 metre = 1000 millimetre
        }

        else // default unit is inch
        {
            return (num * 39.37f); // 1 metre = 39.37 inch
        }
    }
}
