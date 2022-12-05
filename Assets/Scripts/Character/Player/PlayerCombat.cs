using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Player player;
    AudioSource audioSource;
    [SerializeField] int damage = 5;
    [SerializeField] LayerMask attackLayerMask;
    [SerializeField] AudioClip attackAudioClip;
    public GameObject weaponHolder;
    public GameObject currentWeapon;
    public Transform attackPoint;
    public float attackRange;
    public float attackCooldown;
    [HideInInspector] public bool isAttack = false;

    [Header("Weapon Rotate")]
    private Vector3 mousePos;
    private Vector2 direction;

    #region MonoBehaviour Methods

    private void Start()
    {
        player = GetComponent<Player>();
        audioSource = GetComponent<AudioSource>();
        // if (currentWeapon != null)
        //     SetWeapon();
    }

    private void Update()
    {
        RotateAttackPoint();

        if (player.anim.GetFloat("MoveVertical") > 0.1f)
        {
            weaponHolder.transform.position = new Vector3(transform.position.x, transform.position.y, -0.1f);
        }
        else if (player.anim.GetFloat("MoveVertical") < 0.1f)
        {
            weaponHolder.transform.position = new Vector3(transform.position.x, transform.position.y, 0.1f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    #endregion

    #region Public Methods

    public IEnumerator Combat()
    {
        if (isAttack)
            yield break;

        isAttack = true;
        audioSource.PlayOneShot(attackAudioClip);
        player.anim.SetTrigger("Attack");

        Attack();

        yield return new WaitForSeconds(attackCooldown);

        isAttack = false;
    }

    // public void SetWeapon()
    // {
    //     GameObject weapon = Instantiate(currentWeapon, weaponHolder.transform.position, weaponHolder.transform.rotation, weaponHolder.transform);
    //     weapon.transform.localScale = new Vector3(1, 1, 1);
    // }

    #endregion

    #region Private Methods

    void RotateAttackPoint()
    {
        // Rotate attack point basen on player
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (new Vector3(mousePos.x, mousePos.y) - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        attackPoint.transform.position = Quaternion.Euler(0, 0, angle) * new Vector3(0.8f, 0, 0) + transform.localPosition;

        player.anim.SetFloat("AttackHorizontal", direction.x);
        player.anim.SetFloat("AttackVertical", direction.y);
    }

    void Attack()
    {
        // InstantiateWeapon();

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayerMask);
        damage = currentWeapon.GetComponent<Weapon>().damage;
        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().GetDamage(damage);
        }
    }

    void InstantiateWeapon()
    {
        Debug.Log("Spawn Weapon");
        GameObject weapon = Instantiate(currentWeapon, weaponHolder.transform.position + Vector3.back, weaponHolder.transform.rotation, weaponHolder.transform);
        weapon.transform.localScale = new Vector3(1, 1, 1);
        Destroy(weapon, 0.5f);
    }

    #endregion
}
