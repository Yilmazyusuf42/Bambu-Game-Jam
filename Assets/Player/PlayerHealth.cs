using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;       // Oyuncunun maksimum saðlýðý
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;      // Oyuncunun saðlýðý baþlangýçta maksimum
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Oyuncu ölümü burada iþlenir (örneðin, oyun sonu ekraný, yeniden baþlatma vb.)
    }
}
