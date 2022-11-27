using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stationary : Enemy
{
  [Header("Stationary Management")]
  [SerializeField] float cd;
  [SerializeField] GameObject projectile;
  Animator animator;
  bool playerInRadius;
  bool canShooot = true;
  private void Start()
  {
    animator = GetComponent<Animator>();
  }
  void Update()
  {
    CheckPlayer();
    Dead();
  }
  void CheckPlayer()
  {
    playerInRadius = Physics2D.OverlapCircle(transform.position, radius, LayerMask.GetMask("Player"));
    if (playerInRadius && canShooot)
    {
      animator.SetBool("Attack", true);
      StartCoroutine(ShootCD());
    }
  }
  public void SpreadingPoison()
  {
    Instantiate(projectile, transform.position, Quaternion.identity);
  }
  IEnumerator ShootCD()
  {
    canShooot = false;
    yield return new WaitForSeconds(cd);
    canShooot = true;
  }
}
