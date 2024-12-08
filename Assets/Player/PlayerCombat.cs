using UnityEngine;

public class PlayerCombat : MonoBehaviour,IPlayerCombat
{
    [SerializeField] private AudioClip lightAttack;
    [SerializeField] private AudioClip heavyAttack;

    [SerializeField] private float knockbackForce;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private Transform attackPoint;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] LayerMask enemyLayer;

    AudioSource source;
    Animator animator;
    private IPlayerMovement playerMovement;

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
        playerMovement = GetComponent<IPlayerMovement>();
        source = GetComponent<AudioSource>();
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



    void Attack(float attackRadius, int attackDamage, float screenShakeAmount, bool HeavyAttack)
    {
        Vector2 attackDirection = (Vector2)transform.up;

        Collider2D[] enemiesHit = Physics2D.OverlapCircleAll(attackPoint.position, attackRadius, enemyLayer);

        foreach (Collider2D enemy in enemiesHit)
        {
            if (enemy.TryGetComponent<EnemyHealth>(out var enemyHP))
            {
                enemyHP.TakeDamage(attackDamage);
                ScreenShake.Instance.Shake(screenShakeAmount, 0);

                if (enemy.TryGetComponent<enemy>(out var kb))
                {
                    kb.KnockBack(transform.position, knockbackForce);
                }

                GameObject blood = Instantiate(bloodEffect, enemy.transform.position, Quaternion.identity);
                if (HeavyAttack)
                {

                    blood.transform.localScale *= 1.5f;
                    source.PlayOneShot(heavyAttack);
                }
                else
                {
                    source.PlayOneShot(lightAttack);
                }

                Destroy(blood,.2f);
            }
        }
    }


    public void LightAttackAnimationEvent()
    {
        Attack(attackRadius, lightAttackDamage,0.1f,false);
    }

    public void HeavyAttackAnimationEvent()
    {
        Attack(attackRadius, heavyAttackDamage,0.25f,true);
    }


    public void OnLightAttackPerformed()
    {
        if (!lightAttackIsOnCooldown)
        {
            if (playerMovement.IsDodging)
            {
                animator.SetTrigger(AnimationKey.Player_Dash_Strike);
            }
            else
            {
                animator.SetTrigger(AnimationKey.Player_Light_Attack);
            }

            //Attack(attackRadius, lightAttackDamage);
            lightAttackIsOnCooldown = true;
        }

    }

    public void OnHeavyAttackPerformed()
    {
        if (!heavyAttackIsOnCooldown)
        {
            //Attack(attackRadius, heavyAttackDamage);
            animator.SetTrigger(AnimationKey.Player_Heavy_Attack);
            heavyAttackIsOnCooldown = true;

        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, attackRadius);
    }
}
