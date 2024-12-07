using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CrewMember : MonoBehaviour
{
    public static Action<CrewMember> OnCrewMemberSaved;

    [SerializeField] private List<Transform> movePoints;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject uiPrefab;
    [SerializeField] private Canvas worldSpaceCanvas;
    [SerializeField] private float yOffSet = 2f;

    private GameObject instantiatedUI;
    private Transform target;
    private int targetCount =0;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 initialScale;
    private bool isMoving;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        initialScale = transform.localScale;

        instantiatedUI = Instantiate(uiPrefab,worldSpaceCanvas.transform);
        instantiatedUI.gameObject.SetActive(false);
        SetUIPosition();
    }



    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 dir = (target.position - transform.position).normalized;

        if (target.position.x >= transform.position.x)
        {
            transform.localScale = initialScale;
        }
        else
        {
            transform.localScale = new Vector2(-initialScale.x, initialScale.y);
        }



        if (dir != Vector3.zero)
        {
            transform.position += moveSpeed * Time.deltaTime * dir;
            animator.SetBool(AnimationKey.Is_Running, true);
        }
        else
        {
            animator.SetBool (AnimationKey.Is_Running, false);
        }


        if (Vector2.Distance(transform.position, target.position) <= 0.1f)
        {
            
            ++targetCount;
            if (targetCount < movePoints.Count)
            {
                target = movePoints[targetCount];
            }
            else
            {
                AddToCrew();
                animator.SetBool("isReached", true);
            }
        }



        if (instantiatedUI.gameObject.activeInHierarchy)
        {
            SetUIPosition();
        }
    }

    public void StopRunningAnimation()
    {
        animator.SetBool(AnimationKey.Is_Running, false);
    }


    void SetUIPosition()
    {
        instantiatedUI.transform.position = transform.position + new Vector3(0, yOffSet,0);
    }

    private void OnCrewMemberRecruited()
    {
        target = movePoints[targetCount];
        instantiatedUI.SetActive(false);
        isMoving = true;
    }

    private void AddToCrew()
    {
        target = null;
        OnCrewMemberSaved?.Invoke(this);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPlayerRecruit>(out var player) && !isMoving)
        {
            player.CanRecruit = true;
            instantiatedUI.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IPlayerRecruit>(out var player))
        {
            player.CanRecruit = false;
            instantiatedUI.gameObject.SetActive(false);
        }
    }


    private void OnEnable()
    {
        PlayerRecruit.OnCrewMemberRecruited += OnCrewMemberRecruited;
    }

    private void OnDisable()
    {
        PlayerRecruit.OnCrewMemberRecruited -= OnCrewMemberRecruited;
    }


}
