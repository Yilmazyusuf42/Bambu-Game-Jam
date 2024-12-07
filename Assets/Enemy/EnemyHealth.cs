using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public float maxHealth = 100;

    private float currentHealth;

    void Start()
    {
        // Baþlangýçta düþmanýn canýný maksimum deðerine ayarla.
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        // Hasar alýndýðýnda caný düþür.
        currentHealth -= damage;
        Debug.Log($"{gameObject.name} has {currentHealth} health remaining.");


        // Can sýfýr veya daha azsa düþmaný öldür.
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{gameObject.name} has died!");
        // Düþmaný sahneden kaldýr.
        Destroy(gameObject);
    }
}
