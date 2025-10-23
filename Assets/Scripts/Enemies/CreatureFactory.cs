using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CreatureFactory : MonoBehaviour
{
    public abstract GameObject SpawnICreature(Vector3 position);
}
