using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUp : MonoBehaviour, IPickUp
{
    // Start is called before the first frame update

    BubblePop popScript;

    [SerializeField] private GameObject[] pickups;
    private Transform lookAtTarget;
    //[SerializeField] private 

    public static float[] dropChance = new float[6];
    public static float sum;

    public enum PickUpType
    {
        MAGSIZE,
        FIRERATE,
        BULLETSPREAD,
        BULLETCOUNT,
        PROJECTILESPEED,
        HEALTHBONUS,
        MOREAMMO
    }

    [SerializeField] private PickUpType type;
    private void Awake()
    {
       
    }

    public PickUpType Collect()
    {
        return type;
    }
    public void PopThisBubble()
    {
        print("pop the bubble");
        popScript.Pop();
    }
    private void Update()
    {
        if(lookAtTarget == null)return;
        transform.LookAt(lookAtTarget);
        transform.localRotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
    
    public void Initialize()
    {
        foreach(GameObject item in pickups) item.SetActive(false);
        if (lookAtTarget == null) lookAtTarget = GameManager.instance.GetPlayer().transform;
        popScript = GetComponentInChildren<BubblePop>();
        popScript.popped = () => Destroy(gameObject);


        float temp = Random.Range(0f, sum);

        int pickUpNum = 0;
        if (temp < dropChance[0]) pickUpNum = 0;

        for (int i = 0; i < 5; i++)
        {
            
            if (dropChance[i] <= temp && dropChance[i + 1] > temp)
            {
                pickUpNum = i + 1;
                //print("hit");
            }
        }
     
        //read all lines add 
        switch (pickUpNum)
        {
            case 0:
                type = PickUpType.MAGSIZE;
                
                break;
            case 1:
                type = PickUpType.FIRERATE; 
                break;
            case 2:
                type = PickUpType.BULLETSPREAD;
                break;
            case 3:
                type = PickUpType.PROJECTILESPEED;
                break;
            case 4: 
                type = PickUpType.HEALTHBONUS;
                break;
            case 5:
                type = PickUpType.MOREAMMO;
                break;
        }
        pickups[pickUpNum].SetActive(true);
     //   print("I exist!");
    }
}
