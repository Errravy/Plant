using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    private int health = 100;
    private int gold;

    public int Health
    {
        get { return health; }
        set
        {
            if (value < 0) health = 0;
            else health -= value;
        }
    }

    public int Gold
    {
        get { return gold; }
        set
        {
            if (value < 0) gold = 0;
            else gold += value;
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void AddItem(GameObject item)
    {
        PlayerCombat playerCombat = GetComponent<PlayerCombat>();
        playerCombat.currentWeapon = item;
        // playerCombat.SetWeapon();
    }

    public void RestoreHealth(int amount)
    {
        health += amount;
    }
}
