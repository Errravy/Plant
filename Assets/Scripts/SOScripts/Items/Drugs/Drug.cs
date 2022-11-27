using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Drug", menuName = "Inventory/Items/Drug")]
public class Drug : ItemSO, IConsumeable
{
  public int healthRestoreAmount;

  private void Awake()
  {
    type = ItemType.Consumable;
  }

  public void Consume()
  {
    Player player = FindObjectOfType<Player>();
    // Restore health
    if (healthRestoreAmount > 0)
      PlayerStats.Instance.RestoreHealth(healthRestoreAmount);

    // Remove from inventory
    player.playerBags[0].RemoveItem(this);
  }
}
