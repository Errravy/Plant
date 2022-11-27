using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Talk Quest", menuName = "Quest/TalkQuest")]
public class TalkQuestSO : QuestSO
{
  [Header("Requirements")]
  public string talkTargetName;
  public bool hasTalk;

  private void Awake()
  {
    questTypeAction = QuestTypeAction.TalkToNPC;
  }

  public void CheckTalkTarget(string talkTargetName)
  {
    if (questStatus == QuestStatus.InProgress)
    {
      if (this.talkTargetName == talkTargetName)
        hasTalk = true;
    }
  }

  public override void CheckQuestStatus()
  {
    if (hasTalk)
      questStatus = QuestStatus.Completed;
  }
}
