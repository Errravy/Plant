using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestSave : MonoBehaviour
{
    public static QuestSave Instance;

    public QuestSO currentMainQuest;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        currentMainQuest = GameManager.Instance.level.mainQuests[0];
    }

    private void Update()
    {
        foreach (QuestSO quest in GameManager.Instance.level.mainQuests)
        {
            if (quest.questID == GameManager.Instance.questIndex.questID)
            {
                currentMainQuest = quest;
                break;
            }
        }

        CheckQuestStatus();
    }

    void CheckQuestStatus()
    {
        if (currentMainQuest.questTypeAction == QuestTypeAction.CollectItem)
            ((CollectQuestSO)currentMainQuest).CountItemAmount();

        if (currentMainQuest.questTypeAction == QuestTypeAction.Kill)
            ((KillQuestSO)currentMainQuest).CountKillAmount();

        if (currentMainQuest.questTypeAction == QuestTypeAction.Travel)
            ((TravelQuestSO)currentMainQuest).CheckCurrentScene();

        currentMainQuest.CheckQuestStatus();
    }
}
