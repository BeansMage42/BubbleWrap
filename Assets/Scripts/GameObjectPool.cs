using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class GameObjectPool 
{
    public List<GameObject> pool{get; private set;}
    private GameObject objectToPool;

    public GameObjectPool(GameObject poolObject, int numToPool)
    {

        objectToPool = poolObject;
        pool = new List<GameObject>();
        for (int i = 0; i < numToPool; i++)
        {
            GameObject temp = GameObject.Instantiate(objectToPool);
            temp.SetActive(false);
            pool.Add(temp);
        }
    } 

    public void ReturnToPool(GameObject poolObject)
    {
        poolObject.SetActive(false);
        pool.Add(poolObject);

    }
    public GameObject GetPoolObject()
    {
        GameObject foundObject = null;
        if(pool.Count == 0)
        {
            foundObject = GameObject.Instantiate(objectToPool);
            //Debug.Log("spawn new");
        }
        else
        {
            foundObject = pool[0];
            pool.Remove(foundObject);
            //Debug.Log("found in pool " + pool.Count);
        }
       // Debug.Log("retrieved from pool");
        return foundObject;
    }


}
