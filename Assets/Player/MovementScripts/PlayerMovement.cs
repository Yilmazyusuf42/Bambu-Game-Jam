using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputReceiver))]

public class PlayerMovement : MonoBehaviour,IPlayerMovement
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private Transform driverSeat;
    private bool isDriving;
    private float initialMass;

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

    [Header("*** CLIMB ***")]
    [SerializeField] private float climbSpeed = 5f;
    private bool canClimb;

    private Rigidbody2D rb;
    private Animator animator;
    private float playerScale;

    public bool IsDodging { get => isDodging;}

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
        playerScale = transform.localScale.x;

        initialMass = rb.mass;
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
            transform.localScale = new (playerScale, playerScale);
        }
        else if (rb.velocity.x < -0.1f)
        {
            transform.localScale = new (-playerScale, playerScale);
        }
    }

    private bool GroundCheck()
    {
        return Physics2D.Raycast(raycastShootPoint.position, Vector2.down, rayLength, groundLayer);
    }

    private void Animate()
    {
        animator.SetBool(AnimationKey.Is_Running, !(rb.velocity.x == 0));
    }

    private void Update()
    {
        if (isDriving)
        {
            transform.position = driverSeat.transform.position;
            return;
        }

        isGrounded = GroundCheck();

        if (isDodging)
        {
            PerformDodge();
        }

        if (Time.time >= lastDodgeTime + timeBetweenDodges && !isDodging)
        {
            canDodge = true;
        }

        if (canClimb && Input.GetKey(KeyCode.W))
        {
            Climb();
        }

        Move();
        RotatePlayer();
        Animate();
    }

    void Climb()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y + climbSpeed * Time.deltaTime);

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
            StartDodge();
        }
    }

    private Vector3 GetDodgeDirection()
    {
        Vector2 moveDirection = new(InputReceiver.Instance.GetMoveDirection(), 0);
        //if character does not move dash towards its own direction.Else dash according to movement.
        return !moveDirection.Equals(Vector2.zero) ? new Vector2(moveDirection.x, 0).normalized : new Vector2(-transform.localScale.x, 0).normalized;
    }

    private void StartDodge()
    {
        isDodging = true;
        canDodge = false;
        lastDodgeTime = Time.time;
        dodgeStartTime = Time.time;
        dodgeDirection = GetDodgeDirection();
        animator.SetBool(AnimationKey.Player_Is_Dodging, true);
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
            transform.position += dodgeDirection * Time.deltaTime / dodgeTime * dodgeDistance;
        }
    }

    private void EndDodge()
    {
        animator.SetBool(AnimationKey.Player_Is_Dodging, false);
        isDodging = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            canClimb = false;
        }
    }


    private void OnEnable()
    {
        CarDriveTriggerSystem.OnPlayerStartedToDrive += OnPlayerStartedToDrive;
        CarDriveTriggerSystem.OnPlayerStoppedToDrive += OnPlayerStoppedToDrive;
    }

    private void OnDisable()
    {
        CarDriveTriggerSystem.OnPlayerStartedToDrive -= OnPlayerStartedToDrive;
        CarDriveTriggerSystem.OnPlayerStoppedToDrive -= OnPlayerStoppedToDrive;
    }

    private void OnPlayerStoppedToDrive()
    {
        playerUI.SetActive(true);
        StartCoroutine(SlowApproach(-2));
        rb.mass = initialMass;
        isDriving = false;
    }

    private void OnPlayerStartedToDrive()
    {
        playerUI.SetActive(false);
        transform.position = driverSeat.position;
        StartCoroutine(SlowApproach(2));
        animator.SetBool(AnimationKey.Is_Running, false);
        rb.mass = 0;
        isDriving = true;
    }

    private IEnumerator SlowApproach(float targetIncrease)
    {
        Camera mainCam = Camera.main;
        float initialSize = mainCam.orthographicSize; // Kameranın başlangıç boyutu
        float targetSize = initialSize + targetIncrease; // Hedef boyut
        float elapsedTime = 0f;

        Vector3 initialPosition = mainCam.transform.position; // Kameranın başlangıç pozisyonu

        while (elapsedTime < 1f)
        {
            // Kamera boyutunu düzgün bir şekilde arttır
            mainCam.orthographicSize = Mathf.Lerp(initialSize, targetSize, elapsedTime / 1f);

            // Kamera pozisyonunu ortografik boyuta göre güncelle
            mainCam.transform.position = new Vector3(
                initialPosition.x,
                transform.position.y + mainCam.orthographicSize - initialSize, // Oyuncuyu merkezde tut
                initialPosition.z
            );

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        mainCam.orthographicSize = targetSize;
        mainCam.transform.position = new Vector3(
            initialPosition.x,
            transform.position.y + targetSize - initialSize,
            initialPosition.z
        );
    }
}


