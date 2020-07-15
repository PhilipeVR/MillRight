using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DRO_Panel_Interactable_Manager : MonoBehaviour
{
    [SerializeField] DRO_ButtonState XButton, YButton, HeadButton, QuillFeedButton, FineAdjustmentButton;
    [SerializeField] public string intro, drilling, sidemilling, facemilling;
    [SerializeField] public GameObject Vise1, Vise2, Vise3;
    [SerializeField] public XWheelControl tmp1;
    [SerializeField] public YWheelControl tmp2;
    [SerializeField] public ZWheelControl tmp3;
    [SerializeField] public QuillFeedControl tmp4;

    // Start is called before the first frame update
    void Start()
    {
        NoInteraction();
    }

    // Update is called once per frame
    public void SetupDRO(string dialogueName)
    {
        NoInteraction();

        if (dialogueName.Equals(drilling))
        {

            Vise1.SetActive(true);
            Vise2.SetActive(false);
            Vise3.SetActive(false);
            HeadButton.interactable = true;
            QuillFeedButton.interactable = true;
            FineAdjustmentButton.interactable = true;

        }
        else if (dialogueName.Equals(sidemilling))
        {
            Vise1.SetActive(false);
            Vise2.SetActive(true);
            Vise3.SetActive(false);
            tmp3.resetAnim(1);
            tmp4.resetAnim(0);
            YButton.interactable = true;
            QuillFeedButton.interactable = true;
            FineAdjustmentButton.interactable = true;
        }
        else if (dialogueName.Equals(facemilling))
        {
            tmp2.resetAnim(0);
            tmp4.resetAnim(0);
            tmp3.resetAnim(1);
            Vise1.SetActive(false);
            Vise2.SetActive(false);
            Vise3.SetActive(true);
            XButton.interactable = true;
            QuillFeedButton.interactable = true;
            FineAdjustmentButton.interactable = true;
        }


    }

    void NoInteraction()
    {
        XButton.interactable = false;
        YButton.interactable = false;
        HeadButton.interactable = false;
        QuillFeedButton.interactable = false;
        FineAdjustmentButton.interactable = false;
    }

    void jumpTime(Animator anim, float sec)
    {
        anim.Play(anim.runtimeAnimatorController.animationClips[0].name, 0, sec);
    }
}

