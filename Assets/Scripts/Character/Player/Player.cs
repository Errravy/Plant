using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public HealthPlayer SO;
    [Header("Character Stat")]
    public int health;
    bool poison = false;
    public static int gold;
    int getDamage;
    public bool meja = false;
    [HideInInspector] public Animator anim;


    [Header("Inventory")]
    public BagSO[] playerBags;

    [Header("Store GameObjects")]
    public GameObject currentNPC;
    [HideInInspector] public GameObject currentDoor;
    public GameObject currentCollectable;
    public GameObject currentPlant;

    #region MonoBehaviour Methods

    private void Start()
    {
        anim = GetComponent<Animator>();
        health = SO.health;
    }
    private void Update()
    {
        SO.health = health;
        if (health <= 0)
        {
            Debug.Log("Tewas");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
            currentNPC = other.gameObject;

        if (other.gameObject.CompareTag("Door"))
            currentDoor = other.gameObject;

        if (other.gameObject.CompareTag("Collectable"))
            currentCollectable = other.gameObject;

        if (other.gameObject.CompareTag("Plant"))
            currentPlant = other.gameObject;

        if (other.gameObject.CompareTag("Meja"))
            meja = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            // currentNPC.GetComponent<NPC>().CloseShop();
            Invoke("NpcNull", 0.5f);

        }
        if (other.gameObject.CompareTag("Meja"))
        {
            meja = false;
        }
        if (other.gameObject.CompareTag("Door"))
            currentDoor = null;

        if (other.gameObject.CompareTag("Collectable"))
            currentCollectable = null;
    }
    void NpcNull()
    {
        currentNPC = null;
    }
    #endregion

    #region Public Methods

    public void GetDamage(int damage)
    {
        health -= damage;
    }
    public void GetPoisoned(int damagePerSecond)
    {
        StartCoroutine(Poisoned(damagePerSecond));
    }
    public void StopPoison()
    {
        getDamage = 0;
    }
    public IEnumerator Poisoned(int damagePerSecond)
    {
        getDamage = 3;
        while (getDamage >= 0)
        {
            yield return new WaitForSeconds(2);
            if (getDamage <= 0) { break; }
            health -= damagePerSecond;
            getDamage--;
        }
    }
    #endregion

    #region Private Methods


    #endregion
}
