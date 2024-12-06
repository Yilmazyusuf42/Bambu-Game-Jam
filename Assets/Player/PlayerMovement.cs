using UnityEngine;

[RequireComponent(typeof(InputReceiver))]

public class PlayerMovement : MonoBehaviour,IPlayerMovement
{

    [Header("*** MOVE ***")]
    [SerializeField] protected float moveSpeed;
    private bool isRunning;

    [Header("*** JUMP ***")]
    [SerializeField] private Transform raycastShootPoint;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float rayLength;
    [SerializeField] protected float jumpSpeed;
    protected bool isGrounded;


    [Header("*** DODGE ***")]
    [SerializeField] private float dodgeDistance = 10f;
    [SerializeField] private float dodgeTime = .3f;
    [SerializeField] private float timeBetweenDodges = 1f;
    private bool isDodging;
    private float dodgeStartTime;
    private Vector3 dodgeDirection;
    private bool canDodge = true;
    private float lastDodgeTime = -Mathf.Infinity;



    private Rigidbody2D rb;
    private Animator animator;
    private float playerScale;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();

        playerScale = transform.localScale.x;  
    }
    private void Move()
    {
        float moveDir = InputReceiver.Instance.GetMoveDirection();

        rb.velocity = new Vector2(moveDir * moveSpeed,rb.velocity.y);

        
    }


    private void RotatePlayer()
    {
        if (rb.velocity.x > 0.1f)
        {
            transform.localScale = new (-playerScale, playerScale);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new (playerScale, playerScale);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.Raycast(raycastShootPoint.position, Vector2.down, rayLength, groundLayer);
    }

    private void Animate()
    {
        //animator.SetBool("isRunning", !(rb.velocity.x == 0));
    }

    private void FixedUpdate()
    {
        isGrounded = GroundCheck();

        if (isDodging)
        {
            PerformDodge();
        }

        if (Time.time >= lastDodgeTime + timeBetweenDodges && !isDodging)
        {
            canDodge = true;
        }

        Move();
        RotatePlayer();
        Animate();
    }

    public void OnJumpPerformed()
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public void OnDodgePerformed()
    {
        if (canDodge)
        {
            print("girdi");
            StartDodge();
        }
    }

    private Vector3 GetDodgeDirection()
    {
        Vector2 moveDirection = new(InputReceiver.Instance.GetMoveDirection(), 0);
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector2.zero) ? new Vector2(moveDirection.x, 0) : new Vector2(transform.localScale.x, 0).normalized;
    }

    private void StartDodge()
    {
        isDodging = true;
        canDodge = false;
        lastDodgeTime = Time.time;
        dodgeStartTime = Time.time;
        dodgeDirection = GetDodgeDirection();
        //animator.SetBool("isDodging", true);
    }

    private void PerformDodge()
    {
        float elapsedTime = Time.time - dodgeStartTime;

        if (elapsedTime > dodgeTime)
        {
            EndDodge();
        }
        else
        {
            transform.position += dodgeDirection * Time.fixedDeltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        //animator.SetBool("isDodging", false);
        isDodging = false;
    }
}
