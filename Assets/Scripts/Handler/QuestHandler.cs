using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestHandler : MonoBehaviour
{
    #region Fields

    public TextMeshProUGUI questName;
    public TextMeshProUGUI questDescription;
    public TextMeshProUGUI questRewards;
    public Button acceptButton;
    public Button declineButton;
    public Button closeButton;

    #endregion

    #region MonoBehaviour Methods

    private void Start()
    {
        acceptButton.onClick.AddListener(() => AcceptQuest());
        declineButton.onClick.AddListener(() => DeclineQuest());
        closeButton.onClick.AddListener(() => CloseQuest());
    }

    #endregion

    #region Public Methods

    public void StartQuest(QuestSO questSO)
    {
        questName.text = questSO.questName;
        questDescription.text = questSO.questDescription;

        string itemRewards = "";
        if (questSO.items.Count > 0)
        {
            foreach (GameObject item in questSO.items)
                itemRewards += item.name + "\n";
        }

        questRewards.text = "Rewards: \n" + questSO.gold + "\n" + itemRewards;

        CheckQuestStatus(questSO);
    }

    public void AcceptQuest()
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.buttonClickAudioClip, Camera.main.transform.position);

        GameManager.currentQuestSO.questStatus = QuestStatus.InProgress;
        CloseQuest();
    }

    public void DeclineQuest()
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.buttonClickAudioClip, Camera.main.transform.position);

        CloseQuest();
    }

    #endregion

    #region Private Methods

    void CloseQuest()
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.buttonClickAudioClip, Camera.main.transform.position);

        if (GameManager.currentQuestSO.questStatus == QuestStatus.Completed)
        {
            PlayerStats.Instance.Gold = GameManager.currentQuestSO.gold;

            StoreItem(GameManager.currentQuestSO.items);

            GameManager.currentNPC.GetComponent<NPC>().mainQuests.Remove(GameManager.currentQuestSO);
            GameManager.Instance.questIndex.questID++;
        }

        PlayerInput playerInput = FindObjectOfType<PlayerInput>();
        playerInput.EnableInput();

        gameObject.SetActive(false);
    }

    void CheckQuestStatus(QuestSO questSO)
    {
        if (questSO.questStatus == QuestStatus.NotStarted)
        {
            acceptButton.gameObject.SetActive(true);
            declineButton.gameObject.SetActive(true);
            closeButton.gameObject.SetActive(false);
        }

        if (questSO.questStatus == QuestStatus.InProgress || questSO.questStatus == QuestStatus.Completed)
        {
            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }

        if (questSO.questStatus == QuestStatus.Completed && questSO.questType == QuestType.Main)
        {
            acceptButton.gameObject.SetActive(false);
            declineButton.gameObject.SetActive(false);
            closeButton.gameObject.SetActive(true);
        }
    }

    void StoreItem(List<GameObject> items)
    {
        foreach (GameObject item in items)
        {
            if (item?.GetComponent<Weapon>() is Weapon weapon)
            {
                PlayerStats.Instance.AddItem(weapon.gameObject);
            }
            else
            {
                Player player = FindObjectOfType<Player>();
                var itemSO = item?.GetComponent<ItemDropped>().Item;
                player.playerBags[0].AddItem(itemSO, 1);
            }
        }
    }

    #endregion
}
