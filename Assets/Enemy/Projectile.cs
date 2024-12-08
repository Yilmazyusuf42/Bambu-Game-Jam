using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage = 10f;           // Verilecek hasar miktarý
    public float lifetime = 5f;         // Merminin yaþam süresi

    void Start()
    {
        // Belirli bir süre sonra mermiyi yok et
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer mermi "Player" layer'ýna sahip bir nesneye çarparsa
        if (collision.CompareTag("Player"))
        {
            // Oyuncuya hasar ver
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }

            // Mermiyi yok et
            Destroy(gameObject);
        }
        if (collision.TryGetComponent<TheCar>(out var car))
        {
            car.CartakingDamage(damage);
            
            Destroy(gameObject);
        }
    }
}
