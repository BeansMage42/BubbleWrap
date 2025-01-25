using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update

    BubblePop popScript;
    [SerializeField] private Transform containedObject;

    public enum PickUpType
    {
        MAGSIZE,
        FIRERATE,
        BULLETSPREAD,
        BULLETCOUNT,
        PROJECTILESPEED
    }

    [SerializeField] private PickUpType type;

    private void Start()
    {
        popScript = GetComponentInChildren<BubblePop>();
    }
    public PickUpType Collect()
    {

        popScript.Pop();
        print("collect");
        return type;
    }


    
}
