using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Consumable,
    Equipment,
    Puzzle,
}

public class ItemSO : ScriptableObject
{
    public GameObject graphic;
    public GameObject icon;
    public GameObject item;
    public ItemType type;
    public string itemName;
    [TextArea(10, 10)] public string description;
}

