using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool candrop = true;
    [Header("Enemy Stats")]
    public int health;
    public int damage;

    [Header("Attack Management")]
    public float attackDuration;

    [Header("Enemy Range")]
    public bool drawRadius;
    public float radius;
    public float attackRadius;

    [Header("Drop")]
    public GameObject[] dropPuzzle;
    public void GetDamage(int damage)
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.enemyDamagedAudioClip, transform.position);
        StartCoroutine(FlashRed());

        health -= damage;
    }

    public virtual void Dead()
    {
        int rnd = Random.Range(0, dropPuzzle.Length);
        if (health <= 0 && candrop)
        {
            float chance = Random.Range(0, 101);
            if (chance <= 70)
                Instantiate(dropPuzzle[rnd], transform.position, Quaternion.identity);
            Destroy(gameObject, 0.1f);
            candrop = false;
        }
    }
    void OnDrawGizmos()
    {
        if (drawRadius)
            Gizmos.DrawWireSphere(transform.position, radius);
        if (drawRadius)
            Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    IEnumerator FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
