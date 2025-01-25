using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BloodPool : MonoBehaviour
{
    private DecalProjector _decal;
    private float _amountBled = 0.5f;

    float maxSize;
    
    // Start is called before the first frame update
    void Start()
    {
        _decal = GetComponent<DecalProjector>();
    }

    public void SetSize(float size)
    {
        if (size > 1)
        {
            maxSize = 0.5f;
        }
        else
        {
            maxSize = 2 - size;
        }
    }

    void Update()
    {
        if (_amountBled <= maxSize)
        {
            _amountBled += Time.deltaTime * 0.2f;
            _decal.size = new Vector3(_amountBled * 1, _amountBled * 1, 1);
        }
        else if (_amountBled > 1)
        {
            Destroy(this);
        }
    }
}
