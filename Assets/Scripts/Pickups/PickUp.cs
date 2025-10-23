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
    [SerializeField] private Transform containedObject;

    [SerializeField] private GameObject[] pickups;
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
    
    public PickUpType Collect()
    {

        
        
        return type;
    }
    public void PopThisBubble()
    {
        Destroy(containedObject.gameObject);
        print("pop the bubble");
        popScript.Pop();
    }
    private void Update()
    {
        transform.LookAt(GameManager.instance.GetPlayer().transform.position);
        transform.localRotation = new Quaternion(0, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }
    
    public void Initialize()
    {
        popScript = GetComponentInChildren<BubblePop>();
        containedObject.gameObject.SetActive(false);

        float temp = Random.Range(0f, sum);

        int pickUpNum = 0;
        if (temp < dropChance[0]) pickUpNum = 0;

        for (int i = 0; i < 5; i++)
        {
            print(dropChance[i]);
            print(temp + " temp values");
            print(dropChance[i + 1]);
            if (dropChance[i] <= temp && dropChance[i + 1] > temp)
            {
                pickUpNum = i + 1;
                print("hit");
            }
        }
     
        print(pickUpNum + " pick up num");
        //read all lines add 
        switch (pickUpNum)
        {
            case 0:
                type = PickUpType.MAGSIZE;
                containedObject = Instantiate(pickups[0], containedObject.position, containedObject.rotation).transform;
                break;
            case 1:
                type = PickUpType.FIRERATE; 
                containedObject = Instantiate(pickups[1], containedObject.position, containedObject.rotation).transform;
                break;
            case 2:
                type = PickUpType.BULLETSPREAD;
                containedObject = Instantiate(pickups[2], containedObject.position, Quaternion.identity).transform;
                break;
            case 3:
                type = PickUpType.PROJECTILESPEED;
                containedObject = Instantiate(pickups[3], containedObject.position, containedObject.rotation).transform;
                break;
            case 4: 
                type = PickUpType.HEALTHBONUS;
                containedObject = Instantiate(pickups[4], containedObject.position, containedObject.rotation).transform;
                break;
            case 5:
                type = PickUpType.MOREAMMO;
                containedObject = Instantiate(pickups[5], containedObject.position, containedObject.rotation).transform;
                break;
        }
        print("I exist!");
    }
}
