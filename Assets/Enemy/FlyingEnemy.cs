using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : enemy
{
    [Header("*** Attack Settings ***")]
    public float moveSpeed = 3f;
    public float maxDistance = 7f;
    public float minDistance = 3f;
    public float fireDistance = 4f;
    public float detectionRange = 10f;
    public float attackCooldown = 1f;

    [SerializeField] private Transform shootPoint;

    [Header("*** Projectile Settings ***")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 5f;

    private bool canAttack = true;
    private Transform target;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Oyuncuyu algýla
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));
        Collider2D carCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Car"));

        if (carCollider != null)
        {
            target = carCollider.transform;
            float distanceToCAr = Vector2.Distance(transform.position, target.position);

            if (distanceToCAr > fireDistance)
            {
                MoveTowardsPlayer(target);
            }

            if (distanceToCAr <= fireDistance && canAttack)
            {
                canAttack = false;
                animator.SetTrigger("Attack");

            }
        }
        else if (playerCollider != null)
        {
            target = playerCollider.transform;
            float distanceToPlayer = Vector2.Distance(transform.position, target.position);

            // Oyuncuya olan mesafeye göre hareket et
            if (distanceToPlayer > fireDistance)
            {
                // Oyuncudan uzaksa yaklaþ
                MoveTowardsPlayer(target);
            }

            // Eðer ateþ mesafesindeyse ateþ et
            if (distanceToPlayer <= fireDistance && canAttack)
            {
                canAttack = false;
                animator.SetTrigger("Attack");

            }


            Rotate();
        }
    }

    void MoveTowardsPlayer(Transform player)
    {
        if (isKnockedBack)
        {
            return;
        }

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);


    }

    void Rotate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        Vector3 scale = transform.localScale;
        scale.x = target.position.x > transform.position.x ? -Mathf.Abs(scale.x) : Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    void AttackPlayer()
    {
        StartCoroutine(AttackCooldown(target));
    }

    IEnumerator AttackCooldown(Transform player)
    {
        canAttack = false;

        // Mermi instantiate et ve oyuncuya doðru fýrlat
        GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, Quaternion.identity);
        Vector2 direction = (player.position - transform.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
        // Saldýrý durumu sýfýrlanýr
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;

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
