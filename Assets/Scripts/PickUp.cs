using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    // Start is called before the first frame update

    BubblePop popScript;
    [SerializeField] private Transform containedObject;
    //[SerializeField] private 

    public enum PickUpType
    {
        MAGSIZE,
        FIRERATE,
        BULLETSPREAD,
        BULLETCOUNT,
        PROJECTILESPEED
    }

    [SerializeField] private PickUpType type;

    private void Awake()
    {
        popScript = GetComponentInChildren<BubblePop>();

        int temp = (int)Random.Range(0f, 5f);

        switch (temp)
        {
            case 0:
                type = PickUpType.MAGSIZE; break;
            case 1:
                type = PickUpType.FIRERATE; break;
            case 2:
                type = PickUpType.BULLETSPREAD; break;
            case 3:
                type = PickUpType.BULLETCOUNT; break;
            case 4: 
                type = PickUpType.PROJECTILESPEED; break;
        }
    }
    
    public PickUpType Collect()
    {

        popScript.Pop();
        print("collect");
        return type;
    }

    private void Update()
    {
        transform.LookAt(GameManager.instance.GetPlayer().transform.position);
        transform.localRotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }



}
