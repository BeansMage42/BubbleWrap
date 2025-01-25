using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Limb : MonoBehaviour
{
    [SerializeField] private GameObject bloodPool;
    [SerializeField] private float bleedSpeed;

    private DecalProjector _decal;
    
    private float _amountBled = 0.1f;

    private bool _shouldBleed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        if (_shouldBleed && _amountBled <= 1)
        {
            _amountBled += Time.deltaTime * bleedSpeed;
            _decal.gameObject.transform.localScale = new Vector3(_amountBled, _amountBled, _amountBled);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!_shouldBleed)
        {
            _decal = Instantiate(bloodPool, transform.position, Quaternion.identity).GetComponent<DecalProjector>();
            _decal.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        
            _shouldBleed = true;
        }
    }
}
