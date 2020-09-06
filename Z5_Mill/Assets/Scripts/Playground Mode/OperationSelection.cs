using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationSelection : MonoBehaviour
{
    [SerializeField] private DRO_Manager droManager;
    [SerializeField] private Toggle_On_Off powerButton;
    [SerializeField] private XWheelControl controlX;
    [SerializeField] private YWheelControl controlY;
    [SerializeField] private ZWheelControl controlZ;
    [SerializeField] private QuillFeedControl controlQuill;
    [SerializeField] private FineAdjustmentControl controlFine;
    [SerializeField] private SwitchBit switchBit;
    [SerializeField] private Operation drilling, sideMill, faceMill, current;
    [SerializeField] private List<SelectionToggle> bitButtons;
    [SerializeField] private GameObject warningBox, doneBTN;
    [SerializeField] private ProcessChecker checker;
    // Start is called before the first frame update
    void Start()
    {
        doneBTN.SetActive(false);
        drilling.activate(false);
        sideMill.activate(false);
        faceMill.activate(false);
    }

    public void ChangeOperation(string name)
    {
        if (CheckOperationChange())
        {
            droManager.resetDRO();
            warningBox.SetActive(false);
            controlX.resetAnim(0);
            controlY.resetAnim(0);
            controlZ.resetAnim(0);
            controlQuill.resetAnim(0);
            controlFine.resetAnim(0);

            drilling.activate(false);
            sideMill.activate(false);
            faceMill.activate(false);
            doneBTN.SetActive(false);


            switchBit.Reset();

            foreach(SelectionToggle toggle in bitButtons)
            {
                toggle.Clean();
            }

            PlacePiece resetPiece = null;
            ClampPiece resetClamp = null;
            RevertDestruction revertStock = null;
            if (drilling.Name.Equals(name))
            {
                drilling.activate(true);
                resetPiece = drilling.GetPlacePiece();
                resetClamp = drilling.Vise.GetComponentInChildren<ClampPiece>();
                revertStock = drilling.RevertStockDestruction;
                checker.ChangeListener(drilling.Name);
                current = drilling;
            }
            else if (sideMill.Name.Equals(name))
            {
                sideMill.activate(true);
                resetPiece = sideMill.GetPlacePiece();
                resetClamp = sideMill.Vise.GetComponentInChildren<ClampPiece>();
                revertStock = sideMill.RevertStockDestruction;
                checker.ChangeListener(sideMill.Name);
                current = sideMill;

            }
            else if (faceMill.Name.Equals(name))
            {
                faceMill.activate(true);
                resetPiece = faceMill.GetPlacePiece();
                resetClamp = faceMill.Vise.GetComponentInChildren<ClampPiece>();
                revertStock = faceMill.RevertStockDestruction;
                checker.ChangeListener(faceMill.Name);
                current = faceMill;

            }

            if (resetPiece != null)
            {
                resetPiece.ResetAnim();
                resetPiece.ResetTrigger();
                powerButton.Place = resetPiece;
                controlX.Place = resetPiece;
                controlY.Place = resetPiece;
                controlZ.Place = resetPiece;
            }
            if(resetClamp != null)
            {
                resetClamp.Reset();
                powerButton.Clamp = resetClamp;
                controlZ.Clamp = resetClamp;
            }
            if(revertStock != null)
            {
                revertStock.RevertStock();
            }
        }
    }

    public Operation Current
    {
        get => current;
    }

    public Operation Drilling
    {
        get => drilling;
    }

    public Operation SideMill
    {
        get => sideMill;
    }

    public Operation FaceMill
    {
        get => faceMill;
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
        [SerializeField] private Boolean activated;

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
            activated = val;
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

        public RevertDestruction RevertStockDestruction
        {
            get => stockMaterial.GetComponent<RevertDestruction>();
        }
    }
}
