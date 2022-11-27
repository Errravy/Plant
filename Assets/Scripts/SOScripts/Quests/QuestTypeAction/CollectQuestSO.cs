using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collect Quest", menuName = "Quest/CollectQuest")]
public class CollectQuestSO : QuestSO
{
    [Header("Requirements")]
    public ItemSO itemSO;
    public int expectedItemAmount;
    public int currentItemAmount;

    private void Awake()
    {
        questTypeAction = QuestTypeAction.CollectItem;
    }

    public void CountItemAmount()
    {
        if (questStatus == QuestStatus.InProgress)
        {
            DisplayInventory displayInventory = Resources.FindObjectsOfTypeAll<DisplayInventory>()[0];
            currentItemAmount = displayInventory.GetItemCount(itemSO);
        }
    }

    public override void CheckQuestStatus()
    {
        if (currentItemAmount >= expectedItemAmount)
            questStatus = QuestStatus.Completed;
    }
}
