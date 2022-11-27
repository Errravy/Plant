using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bamboo : Chaser
{
    [SerializeField] float duration;
    [SerializeField] float ChargeAttack;
    float speedAwal;
    bool chase = false;
    float startRadius;
    float attackStartRadius;
    private void Start()
    {
        speedAwal = mSpeed;
        target = FindObjectOfType<Player>().transform;
        startPoint = transform.position;
        animator = GetComponent<Animator>();
        startRadius = radius;
        attackStartRadius = attackRadius;
    }
    void Update()
    {
        CheckPlayer();
        Dead();
        AnimationControl();
    }
    public override void CheckPlayer()
    {
        playerInRadius = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
        inAttackRadius = Physics2D.OverlapCircle(transform.position, attackRadius, LayerMask.GetMask("Player"));
        if (inAttackRadius && canAttack)
        {
            mSpeed = 0;
            canAttack = false;
            animator.SetTrigger("charging");
        }
        else if (chase)
        {
            transform.position = Vector3.MoveTowards(transform.position, FindObjectOfType<Player>().transform.position, mSpeed * Time.deltaTime);
            radius = 0;
            attackRadius = 0;
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
    public IEnumerator Chasing()
    {
        chase = true;
        mSpeed = chaseSpeed;
        yield return new WaitForSeconds(attackDuration);
        chase = false;
        walking = false;
        canMove = false;
        yield return new WaitForSeconds(tired);
        radius = startRadius;
        attackRadius = attackStartRadius;
        mSpeed = speedAwal;
        walking = true;
        canAttack = true;
        canMove = true;
    }
    void AnimationControl()
    {
        animator.SetBool("run", walking);
        animator.SetBool("attack", chase);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.gameObject.GetComponent<Player>();
            player.GetDamage(damage);
            StopCoroutine(Chasing());
            var amimir = tired + Time.time;
            chase = false;
            walking = false;
            canMove = false;
            if (amimir <= Time.time)
            {
                radius = startRadius;
                attackRadius = attackStartRadius;
                mSpeed = speedAwal;
                walking = true;
                canAttack = true;
                canMove = true;
            }

        }
    }
}