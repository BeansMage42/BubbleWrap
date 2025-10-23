using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{

    [Header("Game mechanics")]
    [SerializeField] float maxHealth;
    private float maxH;
    float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        maxH = 1 / maxHealth;

    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        // jelly.intensity = new ClampedFloatParameter(currentHealth * maxH, 0, 0.7f);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UIManager.instance.AdjustHealth(currentHealth / maxHealth);
        if (currentHealth <= 0)
        {
            GameManager.instance.PlayerDied();
        }

    }
}
