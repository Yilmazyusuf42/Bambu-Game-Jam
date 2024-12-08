using System.Collections;
using UnityEngine;

public class LightEnemy : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float moveSpeed = 3f;
    public float attackDelay = 1f; 
    public float attackDamage = 10f;
    public Vector2 attackAreaSize;
    public Transform attackPoint;

    private bool isAttacking = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectionRange, LayerMask.GetMask("Player"));

        if (playerCollider != null && !isAttacking)
        {
            MoveTowardsPlayer(playerCollider.transform);

            if (Vector2.Distance(transform.position, playerCollider.transform.position) <= attackRange)
            {

                animator.SetTrigger("Attack");
            }
        }
    }

    void MoveTowardsPlayer(Transform player)
    {

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

        if (direction.x != 0)
        {
            animator.SetBool(AnimationKey.Is_Running, true);
            Vector3 scale = transform.localScale;
            scale.x = direction.x > 0 ? Mathf.Abs(scale.x) :  - Mathf.Abs(scale.x);
            transform.localScale = scale;
        }
        else
        {
            animator.SetBool(AnimationKey.Is_Running, false);
        }
    }

    public void AttackPlayer()
    {
        if (!isAttacking)
        {
            StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        isAttacking = true;

        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(attackPoint.position, attackAreaSize, 0f, LayerMask.GetMask("Player"));

        foreach (var hitCollider in hitColliders)
        {
            PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                ScreenShake.Instance.Shake(0.1f, 0f);
                playerHealth.TakeDamage(attackDamage);
            }
        }

        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, attackAreaSize);
    }
}
