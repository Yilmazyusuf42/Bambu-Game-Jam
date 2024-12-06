using UnityEngine;

public class PlayerCombat : MonoBehaviour,IPlayerCombat
{
    [SerializeField] private Transform attackPoint;
    [SerializeField] LayerMask enemyLayer;

    Animator animator;


    [Header("*** LIGHT ATTACK ***")]
    [SerializeField] float lightAttackRadius = 1f;
    [SerializeField] int lightAttackDamage = 10;
    [SerializeField] private float lightAttackCooldown = 0.5f;
    private bool lightAttackIsOnCooldown;
    private float elapsedTimeAfterLightAttack = 0f;

    [Header("*** HEAVY ATTACK ***")]
    [SerializeField] private float heavyAttackRadius = 1.25f;
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
        Attack(lightAttackRadius, lightAttackDamage);
    }

    public void HeavyAttackAnimationEvent()
    {
        Attack(heavyAttackRadius, heavyAttackDamage);
    }


    public void OnLightAttackPerformed()
    {
        if (!lightAttackIsOnCooldown)
        {
            animator.SetTrigger("LightAttack");
            lightAttackIsOnCooldown = true;
        }

    }

    public void OnHeavyAttackPerformed()
    {
        if (!heavyAttackIsOnCooldown)
        {
            animator.SetTrigger("HeavyAttack");
            heavyAttackIsOnCooldown = true;
        }

    }
}
