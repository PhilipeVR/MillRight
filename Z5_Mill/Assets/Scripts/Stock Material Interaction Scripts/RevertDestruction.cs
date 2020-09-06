using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class RevertDestruction : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private List<Vector3> destroyedTransformPos;
    [SerializeField] private List<Vector3> destroyedTransformScale;
    [SerializeField] private List<Quaternion> destroyedTransformRot;
    [SerializeField] private float Countdown;
    [SerializeField] private string CollisionTag;
    [SerializeField] private Button doneButton;
    [SerializeField] private int numOfCubesToDestroy;
    private CollisionVisibility collisionVisibility;
    // Start is called before the first frame update
    void Start()
    {
        destroyedTransformPos = new List<Vector3>();
        destroyedTransformRot = new List<Quaternion>();
        destroyedTransformScale = new List<Vector3>();
        collisionVisibility = prefab.GetComponent<CollisionVisibility>();
        if(collisionVisibility != null)
        {
            collisionVisibility.ParentName = name;
            collisionVisibility.CollisionTag = CollisionTag;
            collisionVisibility.Countdown = Countdown;
            collisionVisibility.Timer = Countdown;
            collisionVisibility.FindSpark();
        }
    }

    // Update is called once per frame
    public void SaveTransform(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        destroyedTransformPos.Add(pos);
        destroyedTransformRot.Add(rot);
        destroyedTransformScale.Add(scale);

        if(destroyedTransformPos.Count > numOfCubesToDestroy)
        {
            doneButton.gameObject.SetActive(true);
            doneButton.interactable = true;
        }
    }

    public int CubeDestroyed
    {
        get => destroyedTransformPos.Count;
    }

    public void RevertStock()
    {
        for (int i = 0; i < destroyedTransformPos.Count; i++)
        {
            GameObject cube = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            cube.transform.parent = gameObject.transform;
            cube.transform.localRotation = destroyedTransformRot[i];
            cube.transform.localPosition = destroyedTransformPos[i];
            cube.transform.localScale = destroyedTransformScale[i];
            
        }
        destroyedTransformRot.Clear();
        destroyedTransformPos.Clear();
        destroyedTransformScale.Clear();
    }
}
