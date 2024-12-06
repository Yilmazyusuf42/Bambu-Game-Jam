using UnityEngine;

public class PlayerCombat : MonoBehaviour,IPlayerCombat
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] LayerMask enemyLayer;

    Animator animator;


    [Header("*** LIGHT ATTACK ***")]
    [SerializeField] int lightAttackDamage = 10;
    [SerializeField] private float lightAttackCooldown = 1f;
    private bool lightAttackIsOnCooldown;
    private float elapsedTimeAfterLightAttack = 0f;

    [Header("*** HEAVY ATTACK ***")]
    [SerializeField] private int heavyAttackDamage = 35;
    [SerializeField] private float heavyAttackCooldown = 5f;
    private bool heavyAttackIsOnCooldown;
    private float elapsedTimeAfterHeavyAttack = 0;


    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }


    private void Update()
    {
        if (lightAttackIsOnCooldown)
        {
            elapsedTimeAfterLightAttack += Time.deltaTime;
            if (elapsedTimeAfterLightAttack >= lightAttackCooldown)
            {
                lightAttackIsOnCooldown = false;
                elapsedTimeAfterLightAttack = 0;
            }
        }

        if (heavyAttackIsOnCooldown)
        {
            elapsedTimeAfterHeavyAttack += Time.deltaTime;
            if (elapsedTimeAfterHeavyAttack >= heavyAttackCooldown)
            {
                heavyAttackIsOnCooldown = false;
                elapsedTimeAfterHeavyAttack = 0;
            }
        }
    }



    void Attack(float attackRadius, int attackDamage)
    {
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            //if (enemy.TryGetComponent<Tower>(out var tower))
            //{
            //    tower.TakeDamage(attackDamage);
            //    ScreenShake.Instance.Shake();
            //}
        }
    }


    public void LightAttackAnimationEvent()
    {
        Attack(attackRadius, lightAttackDamage);
    }

    public void HeavyAttackAnimationEvent()
    {
        Attack(attackRadius, heavyAttackDamage);
    }


    public void OnLightAttackPerformed()
    {
        if (!lightAttackIsOnCooldown)
        {
            Attack(attackRadius, lightAttackDamage);
            animator.SetTrigger("LightAttack");
            lightAttackIsOnCooldown = true;
            print("Light Attack Is On Cooldown:"+lightAttackIsOnCooldown);
        }

    }

    public void OnHeavyAttackPerformed()
    {
        if (!heavyAttackIsOnCooldown)
        {
            Attack(attackRadius, heavyAttackDamage);
            animator.SetTrigger("HeavyAttack");
            heavyAttackIsOnCooldown = true;
            print("Heavy Attack Is On Cooldown:" + heavyAttackIsOnCooldown);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
