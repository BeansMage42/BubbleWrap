using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerHealth : MonoBehaviour
{

    [Header("Game mechanics")]
    [SerializeField] float maxHealth;
    [SerializeField] Volume bloodRed;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth.ToString());
        // jelly.intensity = new ClampedFloatParameter(currentHealth * maxH, 0, 0.7f);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager.instance.AdjustHealth(currentHealth / maxHealth);
        bloodRed.weight =  1 - currentHealth/maxHealth;
        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerDied();
        }

    }
}
