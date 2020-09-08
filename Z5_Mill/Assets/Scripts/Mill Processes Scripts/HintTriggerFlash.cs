using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintTriggerFlash : MonoBehaviour
{
    [SerializeField] private DialogueManager manager;
    [SerializeField] private ProcessAnimationController animationController;
    [SerializeField] private Button hint;
    [SerializeField] private Color HintColor;
    [SerializeField] private int NumOfFlashForHint;
    [SerializeField] private float DelayForFlash;
    [SerializeField] private int HintCountdown;
    [SerializeField] private Button proceedBTN;
    [SerializeField] private DROToggle panel;
    [SerializeField] private List<HintFlash.ObjectTriggerInfo> drillingInfo, sideMillingInfo, faceMillingInfo;
    private Boolean[] drillingClicked, sideMillingClicked, faceMillingClicked;

    private int animIndex = -1;
    private List<HintFlash.ObjectTriggerInfo> currentInfo;
    private Boolean[] currentClickedInfo;
    private List<GameObject> currentGameObjects;
    private int sentenceIndex = 0;
    private Boolean state = false;
    private Boolean react;
    private int prevVal = 0;
    private int triggerIndex = -1;
    private float timer = -1;
    private int currentInfoSentence = -1;
    private Image currentImage = null;
    private Color color;
    private int currentClickedIndex;


    private void Start()
    {
        drillingClicked = new Boolean[drillingInfo.Count];
        sideMillingClicked = new Boolean[sideMillingInfo.Count];
        faceMillingClicked = new Boolean[faceMillingInfo.Count];
        hint.interactable = false;
        timer = -1;
    }
    public void SetAnimIndex()
    {
        animIndex = animationController.Index;
        if(animIndex == 0)
        {
            currentClickedInfo = drillingClicked;
            currentInfo = drillingInfo;
            Reset();
        }
        else if (animIndex == 1)
        {
            currentClickedInfo = faceMillingClicked;
            currentInfo = faceMillingInfo;
            Reset();
        }
        else if (animIndex == 2)
        {
            currentClickedInfo = sideMillingClicked;
            currentInfo = sideMillingInfo;
            Reset();
        }
        else
        {
            animIndex = -1;
        }
    }

    private void Reset()
    {
        sentenceIndex = 0;
        prevVal = 0;
        currentGameObjects = new List<GameObject>();
        currentClickedIndex = 0;
        for(int i = 0; i< currentClickedInfo.Length; i++)
        {
            currentClickedInfo[i] = false;
        }
        StopAllCoroutines();
        IncrementAnim();
    }

    private void IncrementAnim()
    {
        timer = -1;
        triggerIndex = -1;
        currentInfoSentence = -1;
        hint.interactable = false;
        proceedBTN.interactable = true;
        react = false;
        foreach (HintFlash.ObjectTriggerInfo info in currentInfo)
        {
            if(sentenceIndex == info.SenteceIndex)
            {
                proceedBTN.interactable = false;
                currentGameObjects = info.TriggerObject;
                triggerIndex = info.TriggerIndex;
                currentInfoSentence = info.SenteceIndex;
                state = true;
                timer = HintCountdown;
                break;
            }
        }
        sentenceIndex++;
    }


    public void LateUpdate()
    {
        if(animIndex > -1)
        {
            
            if (manager.SentenceIndex > prevVal)
            {
                prevVal = manager.SentenceIndex;
                IncrementAnim();
            }
            if (currentInfoSentence != manager.SentenceIndex)
            {
                hint.interactable = false;
            }
            else
            {
                hint.interactable = react;
            }
            if (state && animationController.Operation.TriggerAnimation.CurrentAnimationStatus && currentInfoSentence == manager.SentenceIndex)
            {
                timer -= Time.deltaTime;
                if(timer < 0)
                {
                    hint.interactable = true;
                    state = false;
                    timer = -1;
                    react = true;
                }
            }
            Boolean found = false;
            int i= -1;
            foreach (HintFlash.ObjectTriggerInfo info in currentInfo)
            {
                i++;
                if (manager.SentenceIndex == info.SenteceIndex)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                proceedBTN.interactable = true;
            }
            else
            {
                proceedBTN.interactable = currentClickedInfo[i];
            }
           
        }

    }

    public void GiveHint()
    {
        if (currentGameObjects.Count != 0)
        {
            foreach (GameObject go in currentGameObjects)
            {
                MeshRenderer meshRenderer = go.GetComponent<MeshRenderer>();
                Image image = go.GetComponent<Image>();
                if (meshRenderer != null)
                {
                    Material[] material = meshRenderer.materials;
                    if (material != null && (animationController.Operation.TriggerAnimation.Index == triggerIndex))
                    {
                        StartCoroutine(FlashMesh(material));
                    }
                }
                else if (image != null && (animationController.Operation.TriggerAnimation.Index == triggerIndex))
                {
                    panel.activate(true);
                    StartCoroutine(FlashImage(image));
                }
            }

        }
    }

    public void StopRoutine()
    {
        StopAllCoroutines();
        if(currentImage != null)
        {
            currentImage.color = color;
        }
    }

    public IEnumerator FlashImage(Image image)
    {

        Color basicColor = image.color;
        color = basicColor;
        currentImage = image;
        for (int i = 0; i < NumOfFlashForHint; i++)
        {
            image.color = HintColor;
            yield return new WaitForSecondsRealtime(DelayForFlash);
            image.color = basicColor;
            yield return new WaitForSecondsRealtime(DelayForFlash);
        }
    }

    public IEnumerator FlashMesh(Material[] materials)
    {
        List<Color> basicColors = new List<Color>();
        foreach (Material material in materials)
        {
            basicColors.Add(material.color);
        }

        for (int i = 0; i < NumOfFlashForHint; i++)
        {
            foreach (Material material in materials)
            {
                material.color = HintColor;
            }
            yield return new WaitForSecondsRealtime(DelayForFlash);
            int index = 0;
            foreach (Color color in basicColors)
            {
                materials[index].color = color;
                index++;
            }
            yield return new WaitForSecondsRealtime(DelayForFlash);
        }
    }

    public void ProceedState(Boolean state)
    {
        proceedBTN.interactable = state;
    }

    public void AnimPlayed()
    {
        currentClickedInfo[currentClickedIndex] = true;
        currentClickedIndex++;
    }




}
