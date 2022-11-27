using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayShop : MonoBehaviour
{
    public BagSO puzel;
    [SerializeField] int xStart;
    [SerializeField] int yStart;
    [SerializeField] int numberOfColumn;
    [SerializeField] int xGap;
    [SerializeField] int yGap;
    [SerializeField] int maxInventory;
    List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        CreateDisplay();
    }
    void Update()
    {
        for (int i = 0; i < puzel.container.Count; i++)
        {
            if (items[i] == null)
            {
                puzel.container.Remove(puzel.container[i]);
                items.Remove(items[i]);
            }
            else
            {
                items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < puzel.container.Count; i++)
        {
            items.Add(Instantiate(puzel.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    Vector3 GetPosition(int i)
    {
        return new Vector3((xStart + xGap * (i % numberOfColumn)), (yStart + -yGap * (i / numberOfColumn)), 0);
    }

    public void AddItem(ItemSO getItem, GameObject dropItem)
    {
        if (puzel.container.Count < maxInventory)
        {
            int i = maxInventory - (puzel.container.Count);
            puzel.AddPuzzle(getItem, 1);
            Destroy(dropItem);

            items.Add(Instantiate(puzel.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    public void RemoveItem(Player player, ItemSO getItem, int amount)
    {
        for (int i = 0; i < puzel.container.Count; i++)
        {
            if (puzel.container[i].itemObjects == getItem)
            {
                puzel.container[i].amount -= amount;
                if (puzel.container[i].amount <= 0)
                {
                    puzel.container.Remove(puzel.container[i]);
                    Destroy(items[i]);
                    items.Remove(items[i]);
                }
            }
        }
    }

    public int GetItemCount(ItemSO itemSO)
    {
        int count = 0;

        foreach (var item in puzel.container)
        {
            if (item.itemObjects == itemSO)
                count++;
        }

        return count;
    }
}
