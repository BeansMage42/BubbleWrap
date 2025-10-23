using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePickUpFactory : PickUpFactory
{
    [SerializeField] private GameObject pickUpPrefab;

    public override IPickUp SpawnIPickUp(Vector3 position)
    {
        GameObject pickUpObject = Instantiate(pickUpPrefab, position + Vector3.up, Quaternion.identity);
        PickUp pickUp = pickUpObject.GetComponentInChildren<PickUp>();
        pickUp.Initialize();
        return pickUp;
    }
}
