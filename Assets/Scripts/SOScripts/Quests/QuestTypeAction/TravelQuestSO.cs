using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Travel Quest", menuName = "Quest/TravelQuest")]
public class TravelQuestSO : QuestSO
{
    [Header("Requirements")]
    public string travelTargetSceneName;
    public bool hasTravel;

    private void Awake()
    {
        questTypeAction = QuestTypeAction.Travel;
    }

    public void CheckCurrentScene()
    {
        if (questStatus == QuestStatus.InProgress)
        {
            if (SceneManager.GetActiveScene().name == travelTargetSceneName)
                hasTravel = true;
        }
    }

    public override void CheckQuestStatus()
    {
        if (hasTravel)
            questStatus = QuestStatus.Completed;
    }
}
