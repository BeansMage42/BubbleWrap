using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class TempGore : MonoBehaviour
{
    [SerializeField] private GameObject[] bodyParts;
    [SerializeField] private GameObject bloodPool;

    [SerializeField] private ParticleSystem bloodSplash;
    
    [SerializeField] float spawnChance = 1;
    [SerializeField] float spawnForce = 5;

    [SerializeField] private LayerMask ground;

    private bool hasCol;

    private DecalProjector _decal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pop()
    {
        if (!hasCol)
        {
            hasCol = true;
            foreach (var bodyPart in bodyParts)
            {
                if (Random.Range(0, 1) < spawnChance)
                {
                    Rigidbody rb = Instantiate(bodyPart, transform.position, Random.rotation).GetComponent<Rigidbody>();
                    rb.AddForce(Random.insideUnitSphere * spawnForce, ForceMode.Impulse);
                }
            }

            print("try");
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), new Vector3(0, -1f, 0), out hit, 5,
                    ground))
            {
                print("Win");
                _decal = Instantiate(bloodPool, hit.point + new Vector3(0, 0.5f, 0), Quaternion.Euler(90, 0, 0)).GetComponent<DecalProjector>();
            }
            
            Instantiate(bloodSplash, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
