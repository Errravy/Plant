using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    public static bool showInfo = false;
    public BagSO puzel;
    ItemSO item;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] int xStart;
    [SerializeField] int yStart;
    [SerializeField] int numberOfColumn;
    [SerializeField] int xGap;
    [SerializeField] int yGap;
    [SerializeField] int maxInventory;
    List<GameObject> items = new List<GameObject>();

    private void Start()
    {
        puzel = GameManager.Instance.bag;
    }
    void Update()
    {
        for (int i = 0; i < GameManager.Instance.bag.container.Count; i++)
        {
            if (items[i] == null)
            {
                GameManager.Instance.bag.container.Remove(GameManager.Instance.bag.container[i]);
                items.Remove(items[i]);
            }
            else
            {
                items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
        if (item != null)
        {
            text.text = item.description;
        }
        else
        {
            text.text = "";
        }
    }
    public void GetItem(ItemSO item)
    {
        this.item = item;
    }
    public void CreateDisplay()
    {
        for (int i = 0; i < GameManager.Instance.bag.container.Count; i++)
        {
            items.Add(Instantiate(GameManager.Instance.bag.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    Vector3 GetPosition(int i)
    {
        return new Vector3((xStart + xGap * (i % numberOfColumn)), (yStart + -yGap * (i / numberOfColumn)), 0);
    }

    public void AddItem(ItemSO getItem, GameObject dropItem)
    {
        if (GameManager.Instance.bag.container.Count < maxInventory)
        {
            int i = maxInventory - (maxInventory - GameManager.Instance.bag.container.Count);
            GameManager.Instance.bag.AddPuzzle(getItem, 1);
            Destroy(dropItem);

            items.Add(Instantiate(GameManager.Instance.bag.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    public void RemoveItem(Player player, ItemSO getItem, int amount)
    {
        for (int i = 0; i < GameManager.Instance.bag.container.Count; i++)
        {
            if (GameManager.Instance.bag.container[i].itemObjects == getItem)
            {
                GameManager.Instance.bag.container[i].amount -= amount;
                if (GameManager.Instance.bag.container[i].amount <= 0)
                {
                    GameManager.Instance.bag.container.Remove(GameManager.Instance.bag.container[i]);
                    Destroy(items[i]);
                    items.Remove(items[i]);
                }
            }
        }
    }

    public int GetItemCount(ItemSO itemSO)
    {
        int count = 0;

        foreach (var item in GameManager.Instance.bag.container)
        {
            if (item.itemObjects == itemSO)
                count++;
        }

        return count;
    }
}
