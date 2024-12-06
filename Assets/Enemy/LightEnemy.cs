using System.Collections;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    public float detectionRange = 10f;     // Görüþ mesafesi
    public float attackRange = 2f;        // Saldýrý mesafesi
    public float moveSpeed = 3f;          // Düþman hareket hýzý
    public float attackDelay = 1f;        // Saldýrý gecikmesi
    public float attackDamage = 10f;      // Saldýrý hasarý
    public Vector2 attackAreaSize; // Saldýrý bölgesinin boyutu

    private bool isAttacking = false;     // Saldýrý yapma durumu

    void Update()
    {
        // Düþman görüþ alanýnda oyuncu var mý?
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));

        if (playerCollider != null && !isAttacking)
        {
            // Oyuncu algýlandý, ona doðru hareket et
            MoveTowardsPlayer(playerCollider.transform);

            // Oyuncu saldýrý alanýnda mý?
            if (Vector2.Distance(transform.position, playerCollider.transform.position) <= attackRange)
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

        // Saldýrý bölgesindeki oyuncuyu kontrol et
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        foreach (var hitCollider in hitColliders)
        {
            // Oyuncuya hasar ver
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(attackDamage);
            }
        }

        Debug.Log("Attack!");

        // Saldýrýdan sonra bir süre bekle
        yield return new WaitForSeconds(attackDelay);

        // Saldýrý durumu sýfýrlanýr
        isAttacking = false;
    }

    // Görüþ alanýný ve saldýrý bölgesini görselleþtirmek için yardýmcý fonksiyon
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, attackAreaSize);
    }
}
