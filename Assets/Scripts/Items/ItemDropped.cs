using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    [SerializeField] bool Puzzle;
    [SerializeField] ItemSO item;
    DisplayInventory puzzleBag;
    Player player;

    public ItemSO Item { get => item; set => item = value; }

    private void Start()
    {
        puzzleBag = Resources.FindObjectsOfTypeAll<DisplayInventory>()[0];
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    public void Collect()
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.collectAudioClip, transform.position);

        if (!Puzzle)
        {
            // player.playerBags[0].AddItem(item, 1);
            GameManager.Instance.puzzleInvenWindow.GetComponent<DisplayPuzzleInven>().AddItem(player, item, 1, gameObject);
        }
        else
        {
            // player.playerBags[1].AddItem(item, 1);
            GameManager.Instance.inventoryWindow.GetComponent<DisplayInventory>().AddItem(player, item, 1, gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = null;
        }
    }
}
