using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TempGore : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private GameObject bloodPool;

    [SerializeField] private ParticleSystem bloodSplash;

    [SerializeField] float spawnChance = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        foreach (var bodyPart in bodyParts)
        {
            if (Random.Range(0, 1) < spawnChance)
            {
                Instantiate(bodyPart, transform.position, Quaternion.identity);
            }
        }

        Instantiate(bloodPool, transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
