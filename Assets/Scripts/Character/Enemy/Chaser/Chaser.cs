using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : Enemy
{
    [Header("Speed")]
    public float mSpeed;
    public float chaseSpeed;

    [Header("Chaser Management")]
    public float tired;

    [Header("Etc")]
    [HideInInspector] public Vector3 startPoint;
    [HideInInspector] public Transform target;
    [HideInInspector] public Animator animator;

    [Header("Condition")]
    [HideInInspector] public bool walking;
    [HideInInspector] public bool canAttack = true;
    [HideInInspector] public bool canMove = true;
    [HideInInspector] public bool playerInRadius;
    [HideInInspector]
    public bool inAttackRadius;

    void Start()
    {
        target = FindObjectOfType<Player>().transform;
        animator = GetComponent<Animator>();
        startPoint = transform.position;
    }

    private void Update()
    {
        CheckPlayer();
        animator.SetBool("run", walking);
        Dead();
    }

    public virtual void CheckPlayer()
    {
        playerInRadius = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
        inAttackRadius = Physics2D.OverlapCircle(transform.position, attackRadius, LayerMask.GetMask("Player"));
        if (inAttackRadius && canAttack)
        {
            StartCoroutine(Attack());
        }
        else if (playerInRadius && canMove)
        {
            walking = true;
            ChaseFlip();
            transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<Player>().transform.position, mSpeed * Time.deltaTime);
        }
        else if (!playerInRadius && canMove)
        {
            StartPointFlip();
            transform.position = Vector3.MoveTowards(transform.position, startPoint, mSpeed * Time.deltaTime);
        }
    }

    public virtual IEnumerator Attack()
    {
        walking = false;
        canAttack = false;
        canMove = false;
        animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackDuration);
        walking = true;
        canAttack = true;
        canMove = true;
    }

    public void ChaseFlip()
    {
        if (target.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (target.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void StartPointFlip()
    {
        if (transform.position.x == startPoint.x && transform.position.x < 0)
        {
            walking = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (transform.position.x == startPoint.x && transform.position.x > 0)
        {
            walking = false;
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (startPoint.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (startPoint.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
