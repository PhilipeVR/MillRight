using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class SwitchBit : MonoBehaviour
{
    public GameObject fineAdjustment;
    public List<GameObject> DrillBits;
    public List<GameObject> EndMills;
    public Boolean chuckType = true;

    public GameObject gameObjectManager, drillChuck, endMillHolder;
    GameButtonManager manager;
    FineAdjustmentControl fine_adjust;
    GameObject currentBit;

    int currentIndex;
    int numOfDrillBits;
    int numOfEndMills;

    public Boolean enable;
    Vector3 spawnBitPosition;

    void Start()
    {
        if (enable)
        {
            manager = gameObjectManager.GetComponent<GameButtonManager>();
            fine_adjust = fineAdjustment.GetComponent<FineAdjustmentControl>();

            currentIndex = 0;

            numOfDrillBits = DrillBits.Count;
            numOfEndMills = EndMills.Count;

            for (int i = 0; i < numOfDrillBits; i++)
            {
                DrillBits[i].SetActive(false);
            }

            for (int i = 0; i < numOfEndMills; i++)
            {
                EndMills[i].SetActive(false);
            }

            currentBit = DrillBits[currentIndex];
            currentBit.SetActive(true);

            drillChuck.SetActive(true);
            endMillHolder.SetActive(false);

            spawnBitPosition = currentBit.transform.localPosition;

            fine_adjust.fineAdjustment = currentBit.transform.parent.gameObject;
            manager.currentBit = currentBit;

        }
    }

    public void Switch(string bitTag)
    {
        if (enable)
        {

            if (!manager.state)
            {
                if (!bitTag.Equals(currentBit.tag))
                {
                    Boolean bitTagSearch = findTag(bitTag);
                }
            }
        }
    }

    private Boolean findTag(string tag)
    {
        spawnBitPosition = currentBit.transform.localPosition;

        List<GameObject> listBits;
        int listLength;
        if (chuckType)
        {
            listLength = numOfDrillBits;
            listBits = DrillBits;
        } else
        {
            listLength = numOfEndMills;
            listBits = EndMills;
        }

        for (int i = 0; i<listLength; i++)
        {
            if (tag.Equals(listBits[i].tag))
            {
                Vector3 tmpPos = listBits[i].transform.localPosition;
                currentBit.transform.localPosition = tmpPos;
                currentBit.SetActive(false);
                currentBit = listBits[i];
                currentBit.SetActive(true);
                currentBit.transform.localPosition = spawnBitPosition;
                currentIndex = i;
                fine_adjust.fineAdjustment = currentBit.transform.parent.gameObject;
                manager.currentBit = currentBit;
                return true;
            }
        }
        return false;
    }

    public void changeChuck(Boolean type)
    {
        spawnBitPosition = currentBit.transform.localPosition;

        if (chuckType != type)
        {
            if (type)
            {
                drillChuck.SetActive(true);
                endMillHolder.SetActive(false);

            }
            else
            {
                endMillHolder.SetActive(true);
                drillChuck.SetActive(false);
            }
            currentBit.SetActive(false);

        }
        chuckType = type;
    }

    private void FixedUpdate()
    {
        if (enable)
        {
            spawnBitPosition = currentBit.transform.localPosition;
        }
    }

}
