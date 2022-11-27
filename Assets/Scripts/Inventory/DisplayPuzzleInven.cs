using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayPuzzleInven : MonoBehaviour
{
    [SerializeField] int xStart;
    [SerializeField] int yStart;
    [SerializeField] int numberOfColumn;
    [SerializeField] int xGap;
    [SerializeField] int yGap;
    [SerializeField] int maxInventory;
    List<GameObject> items = new List<GameObject>();

    void Update()
    {
        for (int i = 0; i < GameManager.Instance.puzzleBag.container.Count; i++)
        {
            if (items[i] == null)
            {
                Destroy(items[i]);
                GameManager.Instance.puzzleBag.container.Remove(GameManager.Instance.puzzleBag.container[i]);
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
        for (int i = 0; i < GameManager.Instance.puzzleBag.container.Count; i++)
        {
            items.Add(Instantiate(GameManager.Instance.puzzleBag.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    Vector3 GetPosition(int i)
    {
        return new Vector3((xStart + xGap * (i % numberOfColumn)), (yStart + -yGap * (i / numberOfColumn)), 0);
    }

    public void AddItem(Player player, ItemSO getItem, int amount, GameObject dropItem)
    {
        if (GameManager.Instance.puzzleBag.container.Count < maxInventory)
        {
            int i = maxInventory - (maxInventory - GameManager.Instance.puzzleBag.container.Count);
            player.playerBags[0].AddPuzzle(getItem, amount);
            Destroy(dropItem);

            items.Add(Instantiate(GameManager.Instance.puzzleBag.container[i].itemObjects.graphic, Vector3.zero, Quaternion.identity, transform));
            items[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
        }
    }

    public void RemoveItem(Player player, ItemSO getItem, int amount)
    {
        for (int i = 0; i < GameManager.Instance.puzzleBag.container.Count; i++)
        {
            if (GameManager.Instance.puzzleBag.container[i].itemObjects == getItem)
            {
                GameManager.Instance.puzzleBag.container[i].amount -= amount;
                if (GameManager.Instance.puzzleBag.container[i].amount <= 0)
                {
                    GameManager.Instance.puzzleBag.container.Remove(GameManager.Instance.puzzleBag.container[i]);
                    Destroy(items[i]);
                    items.Remove(items[i]);
                }
            }
        }
    }

    public int GetItemCount(ItemSO itemSO)
    {
        int count = 0;

        foreach (var item in GameManager.Instance.puzzleBag.container)
        {
            if (item.itemObjects == itemSO)
                count++;
        }

        return count;
    }
}
