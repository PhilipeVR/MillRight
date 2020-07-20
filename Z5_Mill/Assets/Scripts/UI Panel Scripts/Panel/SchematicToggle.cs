using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class SchematicToggle : MonoBehaviour
{
    [SerializeField] private GameObject inches, mms;

    public void inchSchematic()
    {
        inches.SetActive(true);
        mms.SetActive(false);
    }

    public void mmsSchematic()
    {
        inches.SetActive(false);
        mms.SetActive(true);
    }
}
