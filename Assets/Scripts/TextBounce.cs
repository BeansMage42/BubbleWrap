using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextBounce : MonoBehaviour
{

    [SerializeField] float maxDistance;
    [SerializeField] float speed;
    Vector3 scale;
    // Start is called before the first frame update
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.PingPong(Time.unscaledTime * speed, maxDistance) + 1);
        transform.localScale = scale * (Mathf.PingPong(Time.unscaledTime * speed, maxDistance) +1f);
    }
}
