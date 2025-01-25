using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Limb : MonoBehaviour
{
    [SerializeField] private GameObject bloodPool;
    [SerializeField] private float bleedSpeed;
    [SerializeField] private LayerMask ground;

    private DecalProjector _decal;
    private Rigidbody rb;
    
    private float _amountBled = 0.1f;

    private bool _shouldBleed = false;

    private float _timeStayed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        StartCoroutine(KillAfterTime());
    }
    private void OnCollisionEnter(Collision other)
    {
        if (!other.transform.CompareTag("Cute"))
        {
            _shouldBleed = true;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), new Vector3(0, -1f, 0), out hit, 5,
                    ground))
            {
                _decal = Instantiate(bloodPool, hit.point + new Vector3(0, 0.8f, 0), Quaternion.Euler(90, 0, 0)).GetComponent<DecalProjector>();
                _decal.GetComponent<BloodPool>().SetSize(rb.velocity.magnitude);
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _timeStayed = 0;
    }

    private void OnCollisionStay(Collision other)
    {
        _timeStayed += Time.deltaTime;

        if (_timeStayed > 0.2f)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + new Vector3(0, 1f, 0), new Vector3(0, -1f, 0), out hit, 5,
                    ground))
            {
                _decal = Instantiate(bloodPool, hit.point + new Vector3(0, 0.8f, 0), Quaternion.Euler(90, 0, 0)).GetComponent<DecalProjector>();
                _decal.GetComponent<BloodPool>().SetSize(rb.velocity.magnitude);
            }
        }
    }
    
    IEnumerator KillAfterTime()
    {
        yield return new WaitForSeconds(Random.Range(10, 15));
        Destroy(gameObject);
    }
}
