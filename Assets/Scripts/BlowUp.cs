using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KillAfterTime());
    }

    IEnumerator KillAfterTime()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
