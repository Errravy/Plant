using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpread : MonoBehaviour
{
    [SerializeField] int directHitDamage;
    [SerializeField] int damagePerSecond;
    [SerializeField] float maxRadius;
    [SerializeField] float spreadSpeed;
    [SerializeField] float lifeSpan;

    private void Update()
    {
        if (transform.localScale.x <= maxRadius)
        {
            var increment = spreadSpeed * Time.deltaTime;
            Vector3 speed = new Vector3(increment, increment);
            transform.localScale += speed;
        }
        else
        {
            Destroy(gameObject, lifeSpan);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            var player = other.gameObject.GetComponent<Player>();
            player.GetDamage(directHitDamage);
            player.GetPoisoned(damagePerSecond);
        }
    }
}
