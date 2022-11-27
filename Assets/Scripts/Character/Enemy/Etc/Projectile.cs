using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  [SerializeField] float speed;
  Transform target;
  Rigidbody2D rb;
  Vector3 direction;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    target = FindObjectOfType<Player>().transform;
    direction = (target.position - transform.position).normalized * speed;
  }

  void Update()
  {

    rb.velocity = new Vector2(direction.x, direction.y);
  }
}
