using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : MonoBehaviour
{
    [SerializeField] bool Puzzle;
    [SerializeField] ItemSO item;
    [SerializeField] AudioClip collectAudioClip;
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
        AudioSource.PlayClipAtPoint(collectAudioClip, transform.position);

        if (!Puzzle)
        {
            player.playerBags[0].AddItem(item, 1);
            Destroy(gameObject);
        }
        else
        {
            puzzleBag.AddItem(item, gameObject);
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
