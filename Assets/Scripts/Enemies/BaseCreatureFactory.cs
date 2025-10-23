using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCreatureFactory : CreatureFactory
{
    [SerializeField] private GameObject creaturePrefab;

    public override GameObject SpawnICreature(Vector3 position)
    {
        GameObject creatureObject = Instantiate(creaturePrefab, position + Vector3.up, Quaternion.identity);
        CuteCreature creature = creatureObject.GetComponentInChildren<CuteCreature>();
        creature.Initialize();
        return creature.gameObject;
    }
}
