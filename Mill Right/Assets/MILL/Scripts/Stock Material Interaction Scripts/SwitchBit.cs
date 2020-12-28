﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.ExceptionServices;
using UnityEngine;

public class SwitchBit : MonoBehaviour
{
    [SerializeField] private Toggle_On_Off PowerButton;
    public GameObject fineAdjustment;
    public List<GameObject> DrillBits;
    public List<GameObject> EndMills;
    public SelectionToggle chuckSelection, bitSelection;
    public Boolean chuckType = false;

    public GameObject gameObjectManager, drillChuck, endMillHolder;
    GameButtonManager manager;
    FineAdjustmentControl fine_adjust;
    GameObject currentBit;
    private Boolean first = true;

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

            currentIndex = 0;
            chuckType = true;
            currentBit = DrillBits[0];
            endMillHolder.SetActive(false);
            drillChuck.SetActive(false);

        }
    }

    public void Reset()
    {
        for (int i = 0; i < numOfDrillBits; i++)
        {
            DrillBits[i].SetActive(false);
        }

        for (int i = 0; i < numOfEndMills; i++)
        {
            EndMills[i].SetActive(false);
        }

        currentIndex = 0;
        chuckType = true;
        currentBit = DrillBits[0];
        endMillHolder.SetActive(false);
        drillChuck.SetActive(false);
        bitSelection.Clean();
        chuckSelection.Clean();
    }

    public Boolean CheckState()
    {
        Boolean bitState = false;

        for (int i = 0; i < numOfDrillBits; i++)
        {
            if (DrillBits[i].activeSelf)
            {
                bitState = true;
                break;
            }
        }
        if (!bitState)
        {
            for (int i = 0; i < numOfEndMills; i++)
            {
                if (EndMills[i].activeSelf)
                {
                    bitState = true;
                    break;
                }
            }
        }
        return bitState && (endMillHolder.activeSelf || drillChuck.activeSelf);

    }

    public void Switch(string bitTag)
    {
        if (enable)
        {
            if (PowerButton.PowerState)
            {
                WarningEvents.current.CantChangeCutter();
            }
            else 
            {
                if (!bitTag.Equals(currentBit.tag))
                {
                    Boolean bitTagSearch = findTag(bitTag);
                } 
                else if (first && bitTag.Equals(currentBit.tag) && chuckType && drillChuck.activeSelf)
                {
                    currentBit.SetActive(true);
                    drillChuck.SetActive(true);
                    manager.currentBit = currentBit.transform.GetChild(1).gameObject;
                    chuckSelection.Clicked(drillChuck.name);
                    bitSelection.Clicked(currentBit.name);
                    first = false;
                }

            }
        }
    }

    private Boolean findTag(string tag)
    {
        spawnBitPosition = currentBit.transform.localPosition;
        GameObject holder;
        List<GameObject> listBits;
        int listLength;
        if (chuckType)
        {
            holder = drillChuck;
            listLength = numOfDrillBits;
            listBits = DrillBits;
        } else
        {
            holder = endMillHolder;
            listLength = numOfEndMills;
            listBits = EndMills;
        }
       if (drillChuck.activeSelf || endMillHolder.activeSelf)
        {
            for (int i = 0; i < listLength; i++)
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
                    manager.currentBit = currentBit.transform.GetChild(1).gameObject;
                    bitSelection.Clicked(currentBit.name);
                    holder.SetActive(true);
                    chuckSelection.Clicked(holder.name);
                    return true;
                }
            }
        }
        return false;
    }

    public void changeChuck(Boolean type)
    {
        spawnBitPosition = currentBit.transform.localPosition;
        if (PowerButton.PowerState)
        {
            WarningEvents.current.CantChangeCutter();
        }
        else
        {
            if (chuckType != type)
            {
                if (type)
                {
                    drillChuck.SetActive(true);
                    endMillHolder.SetActive(false);
                    chuckSelection.Clicked(drillChuck.name);
                }
                else
                {
                    endMillHolder.SetActive(true);
                    drillChuck.SetActive(false);
                    chuckSelection.Clicked(endMillHolder.name);
                }
                currentBit.SetActive(false);
                bitSelection.Clean();

            }
            else
            {
                if (type)
                {
                    drillChuck.SetActive(true);
                    chuckSelection.Clicked(drillChuck.name);

                }
                else
                {
                    endMillHolder.SetActive(true);
                    chuckSelection.Clicked(endMillHolder.name);

                }
            }
            chuckType = type;
        }
    }

    public Boolean holderState
    {
        get => endMillHolder.activeSelf || drillChuck.activeSelf;
    }

    public GameObject EndMillHolder
    {
        get => endMillHolder;
    }

    public GameObject DrillChuck
    {
        get => drillChuck;
    }


    private void FixedUpdate()
    {
        if (enable  && currentBit != null)
        {
            spawnBitPosition = currentBit.transform.localPosition;
        }
    }

}
