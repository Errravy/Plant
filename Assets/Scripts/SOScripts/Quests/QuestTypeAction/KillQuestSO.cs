using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kill Quest", menuName = "Quest/KillQuest")]
public class KillQuestSO : QuestSO
{
  [Header("Requirements")]
  // public EnemySO enemySO;
  public int expectedEnemyKillAmount;
  public int currentEnemyKillAmount;

  private void Awake()
  {
    questTypeAction = QuestTypeAction.Kill;
  }

  public void CountKillAmount()
  {
    if (questStatus == QuestStatus.InProgress)
    {
      // currentEnemyKillAmount = EnemyManager.Instance.GetEnemyKillCount(enemySO);
    }
  }

  public override void CheckQuestStatus()
  {
    if (currentEnemyKillAmount >= expectedEnemyKillAmount)
      questStatus = QuestStatus.Completed;
  }
}
