using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Puzzle", menuName = "Inventory/Items/Puzzle")]
public class Puzzle : ItemSO
{
  public PlantType plant;
  public int ID;

  void Awake()
  {
    type = ItemType.Puzzle;
  }
}
