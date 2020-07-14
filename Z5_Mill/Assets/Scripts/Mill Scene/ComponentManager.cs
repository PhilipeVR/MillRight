using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ComponentManager : MonoBehaviour
{
    private List<ComponentDetail> details;
    [SerializeField] private PartDataScriptable partData;
    [SerializeField] private GameObject continueBTN;
    [SerializeField] private Text namePart, info, partsExamined, totalNumOfParts;
    [SerializeField] private Image infoImage;

    public Boolean language = true;
    private int currentIndex = -1;
    private int counter;
    private int numOfParts;
    private string partsExam = "Number of parts examined: ";
    private string totalParts = "Total number of parts: ";

    // Start is called before the first frame update
    void Awake()
    {
        details = partData.components;
        counter = 0;
        numOfParts = details.Count;
        totalNumOfParts.text = totalParts + numOfParts.ToString();
        continueBTN.SetActive(false);
        updateCounter();
    }


    public void SetDetails(int index)
    {
        if (language)
        {
            namePart.text = details[index].partName;
            info.text = details[index].sentence;

        } else
        {
            namePart.text = details[index].partNameFR;
            info.text = details[index].sentenceFR;
        }

        infoImage.sprite = details[index].image;
        currentIndex = index;
    }

    public void ChangeLanguage()
    {
        if (language)
        {
            namePart.text = details[currentIndex].partNameFR;
            info.text = details[currentIndex].sentenceFR;
        } else
        {
            namePart.text = details[currentIndex].partName;
            info.text = details[currentIndex].sentence;
        }

        language = !language;
    }

    public void incrementPartCounter()
    {
        counter++;
        updateCounter();
        if(counter == numOfParts)
        {
            continueBTN.SetActive(true);
        }
    }

    public void updateCounter()
    {
        partsExamined.text = partsExam + counter.ToString();
    }
}
