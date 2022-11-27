using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    #region Fields
    [SerializeField] GameObject pocket;
    [Header("Quests")]
    public List<QuestSO> mainQuests;
    public List<QuestSO> sideQuests;

    [Header("Notification")]
    public GameObject notification;
    // Notif sprites
    // 0. Not Started
    // 1. In Progress
    // 2. Completed
    public List<Sprite> questNotifSprites;

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {
    }

    private void Update()
    {
        CheckQuestStatus();
        // CheckQuestActive();
        // UpdateNotification();
    }

    #endregion

    #region Public Methods

    public void Interact()
    {
        // If quest is NotStarted, start dialogue
        if (mainQuests[0].questStatus == QuestStatus.NotStarted)
            GameManager.Instance.ShowDialogue(gameObject, mainQuests[0]);

        // If quest is InProgress, show quest window
        if (mainQuests[0].questStatus == QuestStatus.InProgress)
            GameManager.Instance.ShowQuest(gameObject, mainQuests[0], QuestStatus.InProgress);

        if (mainQuests[0].questStatus == QuestStatus.Completed)
        {
            // If quest is Completed, check if the DialogueType was quest
            Player.gold += 50;
            if (mainQuests[0].dialogueSO.dialogueType == DialogueType.Normal)
                GameManager.Instance.ShowDialogue(gameObject, mainQuests[0]);
            else
                GameManager.Instance.ShowDialogue(gameObject, mainQuests[0], true);
        }
    }

    #endregion

    #region Private Methods

    void UpdateNotification()
    {
        if (mainQuests.Count > 0)
        {
            if (mainQuests[0].questStatus == QuestStatus.NotStarted)
                notification.GetComponent<SpriteRenderer>().sprite = questNotifSprites[0];

            else if (mainQuests[0].questStatus == QuestStatus.InProgress)
                notification.GetComponent<SpriteRenderer>().sprite = questNotifSprites[1];

            else if (mainQuests[0].questStatus == QuestStatus.Completed)
                notification.GetComponent<SpriteRenderer>().sprite = questNotifSprites[2];
        }
        else
            notification.SetActive(false);
    }

    void CheckQuestStatus()
    {
        if (mainQuests.Count > 0)
        {
            if (mainQuests[0].questTypeAction == QuestTypeAction.CollectItem)
                ((CollectQuestSO)mainQuests[0]).CountItemAmount();

            if (mainQuests[0].questTypeAction == QuestTypeAction.Kill)
                ((KillQuestSO)mainQuests[0]).CountKillAmount();

            if (mainQuests[0].questTypeAction == QuestTypeAction.Travel)
                ((TravelQuestSO)mainQuests[0]).CheckCurrentScene();

            mainQuests[0].CheckQuestStatus();
        }

        if (sideQuests.Count > 0)
        {
            foreach (QuestSO quest in sideQuests)
            {
                if (quest.questID == GameManager.Instance.questIndex.questID)
                {
                    if (quest.questTypeAction == QuestTypeAction.CollectItem)
                        ((CollectQuestSO)quest).CountItemAmount();

                    if (quest.questTypeAction == QuestTypeAction.Kill)
                        ((KillQuestSO)quest).CountKillAmount();

                    if (quest.questTypeAction == QuestTypeAction.Travel)
                        ((TravelQuestSO)quest).CheckCurrentScene();

                    quest.CheckQuestStatus();
                }
            }
        }
    }

    void CheckQuestActive()
    {
        if (mainQuests.Count > 0)
        {
            if (GameManager.Instance.questIndex.questID != mainQuests[0].questID)
            {
                notification.SetActive(false);
                GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                notification.SetActive(true);
                GetComponent<Collider2D>().enabled = true;
            }
        }

        if (sideQuests.Count > 0)
        {
            foreach (QuestSO quest in sideQuests)
            {
                if (GameManager.Instance.questIndex.questID == quest.questID)
                {
                    notification.SetActive(true);
                    GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    notification.SetActive(false);
                    GetComponent<Collider2D>().enabled = false;
                }
            }
        }
    }
    public void OpenShop()
    {
        if (pocket != null)
        {
            pocket.SetActive(true);
        }
    }
    public void CloseShop()
    {
        if (pocket != null)
        {
            pocket.SetActive(false);
        }
    }

    #endregion
}
