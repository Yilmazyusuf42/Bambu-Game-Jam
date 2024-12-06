using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    //public float desiredDistance = 5f;     // Oyuncuya korunacak mesafe
    public float moveSpeed = 3f;           // Düþman hareket hýzý
    public float maxDistance = 7f;         // Oyuncuya yaklaþmasý gereken en uzak mesafe
    public float minDistance = 3f;         // Oyuncudan uzaklaþmasý gereken en yakýn mesafe
    public float fireDistance = 4f;        // Ateþ etme mesafesi
    public float detectionRange = 10f;     // Düþmanýn oyuncuyu algýlayacaðý mesafe
    public float attackDelay = 1f;         // Saldýrý gecikmesi

    private bool isAttacking = false;      // Saldýrý durumu

    void Update()
    {
        // Oyuncuyu algýla
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));

        if (playerCollider != null)
        {
            Transform player = playerCollider.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, player.position);

            // Oyuncuya olan mesafeye göre hareket et
            if (distanceToPlayer > fireDistance)
            {
                // Oyuncudan uzaksa yaklaþ
                MoveTowardsPlayer(player);
            }

            // Eðer ateþ mesafesindeyse ateþ et
            if (distanceToPlayer <= fireDistance && !isAttacking)
            {
                AttackPlayer();
            }
        }
    }

    void MoveTowardsPlayer(Transform player)
    {
        // Oyuncuya doðru hareket et
        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }


    void AttackPlayer()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        isAttacking = true;

        // Ateþ etme animasyonu veya etkileþimi buraya ekleyebilirsiniz
        Debug.Log("Firing!");

        // Saldýrýdan sonra bir süre bekle
        yield return new WaitForSeconds(attackDelay);

        // Saldýrý durumu sýfýrlanýr
        isAttacking = false;
    }

    // Görüþ alanýný görsel olarak göstermek için yardýmcý bir fonksiyon (editor'de görsel gösterim için)
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, fireDistance);
    }
}
