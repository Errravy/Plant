using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestSO : ScriptableObject
{
  [Header("Dialogue")]
  public DialogueSO dialogueSO;
  public DialogueSO finishDialogueSO;
  public DialogueSO nextObjectiveDialogueSO;

  [Header("Quest")]
  public int questID;
  public string questName;
  [TextArea(3, 10)] public string questDescription;

  public QuestType questType;
  public QuestTypeAction questTypeAction;
  public QuestStatus questStatus;

  [Header("Rewards")]
  public int gold;
  public List<GameObject> items;

  public abstract void CheckQuestStatus();
}

public enum QuestType
{
  Main,
  Side
}

public enum QuestTypeAction
{
  Kill,
  CollectItem,
  TalkToNPC,
  Travel
}

public enum QuestStatus
{
  NotStarted,
  InProgress,
  Completed
}