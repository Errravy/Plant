using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bag", menuName = "Inventory/Bag")]
public class BagSO : ScriptableObject
{
  public List<InventorySlot> container = new List<InventorySlot>();

  public void AddItem(ItemSO getItem, int getAmount)
  {
    bool hasItem = false;

    foreach (InventorySlot inventory in container)
    {
      if (inventory.itemObjects == getItem && inventory.itemObjects.type != ItemType.Equipment)
      {
        inventory.AddItem(getAmount);
        hasItem = true;
        break;
      }
    }

    if (!hasItem)
    {
      container.Add(new InventorySlot(getItem, getAmount));
    }
  }

  public void RemoveItem(ItemSO getItem)
  {
    foreach (InventorySlot item in container)
    {
      if (item.itemObjects == getItem)
      {
        item.RemoveItem();
        if (item.amount <= 0)
        {
          container.Remove(item);
          break;
        }
      }
    }
  }

  public void AddPuzzle(ItemSO getItem, int getAmount)
  {
    container.Add(new InventorySlot(getItem, getAmount));
  }
}

[System.Serializable]
public class InventorySlot
{
  public int amount;
  public ItemSO itemObjects;
  public InventorySlot(ItemSO getItem, int getAmount)
  {
    amount = getAmount;
    itemObjects = getItem;
  }
  public void AddItem(int value)
  {
    amount += value;
  }

  public void RemoveItem()
  {
    amount--;
  }
}
