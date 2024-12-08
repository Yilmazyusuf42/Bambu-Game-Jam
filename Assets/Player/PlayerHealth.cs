using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Slider slider;
    public float maxHealth = 100f;       // Oyuncunun maksimum saðlýðý
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;      // Oyuncunun saðlýðý baþlangýçta maksimum
        slider.maxValue = maxHealth;
        slider.value = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        Debug.Log("Player Health: " + currentHealth);
        slider.value = currentHealth;
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
