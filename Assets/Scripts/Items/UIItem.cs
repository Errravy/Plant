using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIItem : MonoBehaviour
{
    DisplayPlant display;
    PlantSO plant;
    bool obliterated = false;

    private void Update()
    {
        plant = Resources.FindObjectsOfTypeAll<DisplayPlant>()[0].currentPlant;
    }

    public void Click(Puzzle puzzle)
    {
        PlantType currentPlant = puzzle.plant;
        for (int i = 0; i < plant.puzzles.Count; i++)
        {
            if (!plant.puzzles[i].finished)
                obliterated = plant.puzzles[i].InsertPuzzle(currentPlant, puzzle.ID);
        }
        if (obliterated)
        {
            GameObject ui = gameObject;
            Destroy(ui);
        }
    }

    public void ChangePage(int i)
    {
        display = Resources.FindObjectsOfTypeAll<DisplayPlant>()[0];

        if (display.currentPlantID + i >= 0 && display.currentPlantID + i < display.plant.Count)
            display.currentPlantID += i;
    }
    public void CurePoison(Drug item)
    {
        FindObjectOfType<Player>().StopPoison();
        var bag = GameManager.Instance.puzzleBag;
        bag.RemoveItem(item);
    }
    public void Healing(Drug narkoba)
    {
        var bag = GameManager.Instance.puzzleBag;
        var player = FindObjectOfType<Player>();
        bag.RemoveItem(narkoba);
        if (narkoba.healthRestoreAmount + player.health >= 100)
        {
            player.health = 100;
        }
        else
        {
            player.health += narkoba.healthRestoreAmount;
        }
    }
    public void ShowInformation(ItemSO item)
    {
        GameManager.Instance.inventoryWindow.GetComponent<DisplayInventory>().GetItem(item);
    }
    public void CloseInformation()
    {
        GameManager.Instance.inventoryWindow.GetComponent<DisplayInventory>().GetItem(null);
    }
}
