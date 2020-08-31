using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject diamondPrefab;
    [SerializeField] protected int health;
    [SerializeField] protected int speed;
    [SerializeField] protected int gems;
    [SerializeField] protected Transform pointA, pointB;

    protected Vector3 currentTarget;
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    protected bool isHit = false;
    protected bool isDead = false;

    protected Player player;

    private void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && animator.GetBool("InCombat") == false)
            return;
        if (isDead == false)
            Movement();
    }

    public virtual void Init()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player") == null ? null : GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public virtual void Movement()
    {
        if (currentTarget == pointA.position)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }

        if (transform.position == pointA.position)
        {
            currentTarget = pointB.position;
            animator.SetTrigger("Idle");
        }
        else if (transform.position == pointB.transform.position)
        {
            currentTarget = pointA.position;
            animator.SetTrigger("Idle");
        }


        float distance = Vector3.Distance(transform.localPosition, player.transform.localPosition);

        if (distance > 8.0f)
        {
            isHit = false;
            animator.SetBool("InCombat", false);
        }
        else
        {
            if (distance <= 3f)
                animator.SetBool("InCombat", true);
        }

        Vector3 direction = player.transform.localPosition - transform.localPosition;

        if (direction.x > 0 && distance < 8f)
        {
            spriteRenderer.flipX = false;
        }
        else if (direction.x < 0 && distance < 8f)
        {
            spriteRenderer.flipX = true;
        }

        if (isHit == false && distance > 8.0f)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
        }
        else
        {
            if (distance > 3f)
            {
                transform.position = Vector3.MoveTowards
                (
                    transform.position,
                    new Vector3(player.transform.localPosition.x, currentTarget.y, currentTarget.z), speed * Time.deltaTime
                );
            }

        }

    }

}
