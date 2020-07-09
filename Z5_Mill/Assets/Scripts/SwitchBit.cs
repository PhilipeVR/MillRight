using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBit : MonoBehaviour
{
    public List<GameObject> DrillBitsEndMills;
    public GameObject gameObjectManager;
    GameButtonManager manager;
    GameObject currentBit;
    int currentIndex;
    int numOfBits;
    public Boolean enable;
    public Vector3 spawnPosition;

    void Start()
    {
        if (enable)
        {
            manager = gameObjectManager.GetComponent<GameButtonManager>();
            currentIndex = 0;
            numOfBits = DrillBitsEndMills.Count;
            currentBit = DrillBitsEndMills[currentIndex];
            currentBit.SetActive(true);
            currentBit.transform.localPosition = spawnPosition;
            manager.currentBit = currentBit;

            for(int i = 0; i<numOfBits; i++)
            {
                if(i != currentIndex)
                {
                    DrillBitsEndMills[i].SetActive(false);
                }
            }

        }
    }

    public void Switch()
    {
        if (enable)
        {
            if (!manager.state)
            {
                spawnPosition = currentBit.transform.localPosition;
                currentBit.SetActive(false);

                if(currentIndex+1 < numOfBits)
                {
                    currentBit = DrillBitsEndMills[currentIndex + 1];
                    currentBit.SetActive(true);
                    Vector3 tmpPos = currentBit.transform.localPosition;
                    currentBit.transform.localPosition = spawnPosition;
                    DrillBitsEndMills[currentIndex].transform.localPosition = tmpPos;
                    manager.currentBit = currentBit;
                    currentIndex++;
                }
                else
                {
                    currentBit = DrillBitsEndMills[0];
                    currentBit.SetActive(true);
                    Vector3 tmpPos = currentBit.transform.localPosition;
                    currentBit.transform.localPosition = spawnPosition;
                    DrillBitsEndMills[currentIndex].transform.localPosition = tmpPos;
                    manager.currentBit = currentBit;
                    currentIndex=0;
                }
            }
        }
    }

    void Switch(string bitTag)
    {
        if (enable)
        {
            if (!manager.state)
            {
                spawnPosition = currentBit.transform.position;
                currentBit.SetActive(false);
                for (int i = 0; i < numOfBits; i++)
                {
                    if (bitTag.Equals(DrillBitsEndMills[i].tag))
                    {
                        currentBit = DrillBitsEndMills[i];
                        currentBit.SetActive(true);
                        Vector3 tmpPos = currentBit.transform.position;
                        currentBit.transform.position = spawnPosition;
                        DrillBitsEndMills[currentIndex].transform.position = tmpPos;
                        currentIndex = i;
                        manager.animObject = currentBit;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (enable)
        {
            spawnPosition = currentBit.transform.localPosition;
        }
    }

}
