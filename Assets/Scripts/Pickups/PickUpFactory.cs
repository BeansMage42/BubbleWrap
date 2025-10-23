using UnityEngine;

public abstract class PickUpFactory : MonoBehaviour
{
    public abstract IPickUp SpawnIPickUp(Vector3 position);
}