using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public int maxHealth = 100;

    private int currentHealth;

    void Start()
    {
        // Baþlangýçta düþmanýn canýný maksimum deðerine ayarla.
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
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
