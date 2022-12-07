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
    public GameObject currentDoor;
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
        AudioSource.PlayClipAtPoint(SFX.Instance.playerDamagedAudioClip, transform.position);

        StartCoroutine(FlashRed());

        health -= damage;

        if (health <= 0)
        {
            health = 0;
            // gameObject.SetActive(false);
            GameManager.Instance.GameOver();
        }
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

            if (getDamage <= 0) break;

            StartCoroutine(FlashRed());

            health -= damagePerSecond;
            getDamage--;
        }
    }
    #endregion

    #region Private Methods

    IEnumerator FlashRed()
    {
        GetComponent<SpriteRenderer>().color = Color.red;

        yield return new WaitForSeconds(0.1f);

        GetComponent<SpriteRenderer>().color = Color.white;
    }

    #endregion
}
