using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationSelection : MonoBehaviour
{
    [SerializeField] private XWheelControl controlX;
    [SerializeField] private YWheelControl controlY;
    [SerializeField] private ZWheelControl controlZ;
    [SerializeField] private QuillFeedControl controlQuill;
    [SerializeField] private FineAdjustmentControl controlFine;
    [SerializeField] private Operation drilling, sideMill, faceMill;
    // Start is called before the first frame update
    void Start()
    {

        drilling.activate(false);
        sideMill.activate(false);
        faceMill.activate(false);
    }

    public void ChangeOperation(string name)
    {
        if (CheckOperationChange())
        {
            controlX.resetAnim(0);
            controlY.resetAnim(0);
            controlZ.resetAnim(0);
            controlQuill.resetAnim(0);
            controlFine.resetAnim(0);

            drilling.activate(false);
            sideMill.activate(false);
            faceMill.activate(false);

            PlacePiece resetPiece = null;
            ClampPiece resetClamp = null;
            if (drilling.Name.Equals(name))
            {
                drilling.activate(true);
                resetPiece = drilling.GetPlacePiece();
                resetClamp = drilling.Vise.GetComponentInChildren<ClampPiece>();
            }
            else if (sideMill.Name.Equals(name))
            {
                sideMill.activate(true);
                resetPiece = sideMill.GetPlacePiece();
                resetClamp = sideMill.Vise.GetComponentInChildren<ClampPiece>();

            }
            else if (faceMill.Name.Equals(name))
            {
                faceMill.activate(true);
                resetPiece = faceMill.GetPlacePiece();
                resetClamp = faceMill.Vise.GetComponentInChildren<ClampPiece>();
            }

            if (resetPiece != null)
            {
                resetPiece.ResetAnim();
                resetPiece.ResetTrigger();
            }
            if(resetClamp != null)
            {
                //Debug.Log("Reset Clamp");
                resetClamp.Reset();
            }

        }
    }

    

    private Boolean CheckOperationChange()
    {
        return true;
    }

    [Serializable]
    public struct Operation
    {
        [SerializeField] string name;
        [SerializeField] GameObject dummyCube, stockMaterial, vise;
        private Boolean activated;

        public Boolean Activated
        {
            get => activated;
            set => activated = value;
        }

        public string Name
        {
            get => name;
        }

        public void activate(Boolean val)
        {
            Activated = val;
            dummyCube.SetActive(Activated);
            stockMaterial.SetActive(Activated);
            vise.SetActive(Activated);
        }
        
        public PlacePiece GetPlacePiece()
        {
            return stockMaterial.GetComponent<PlacePiece>();
        }

        public GameObject Vise
        {
            get => vise;
        }
    }
}
