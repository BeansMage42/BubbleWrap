using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    // Start is called before the first frame update

    private TextMeshProUGUI upgradeText;

    float floatSpeed;
    float fadeSpeed;
    Vector3 startPos;

    public void CreateText (string upgradeName,float speed, float time)
    {
        upgradeText.text = upgradeName;
        floatSpeed = speed;
        fadeSpeed = time;
    }
    private void Awake()
    {
        upgradeText = GetComponent<TextMeshProUGUI>();
        startPos = transform.position;
        
        
    }
    private void Update()
    {
        transform.position += (transform.up * floatSpeed);
        if (upgradeText.color.a > 0) {
            upgradeText.color = new Color(0, 0, 0, upgradeText.color.a - fadeSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    
}
