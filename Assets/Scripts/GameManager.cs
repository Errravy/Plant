using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    #region Fields

    public Level level;

    [Header("Quests")]
    public QuestID questIndex;
    public TextMeshProUGUI questInfoDescription;

    public static QuestSO currentQuestSO;
    public static GameObject currentNPC;

    private GameObject questWindow;

    [Header("Dialogue")]
    private GameObject dialogueWindow;

    [Header("Inventory")]
    public BagSO bag;
    public BagSO puzzleBag;
    public GameObject inventoryWindow;
    public GameObject puzzleInvenWindow;

    [Header("Puzzle")]
    private GameObject puzzleWindow;


    [Header("Puzzle")]
    private GameObject pauseWindow;

    #endregion

    #region MonoBehaviour Methods

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        dialogueWindow = Resources.FindObjectsOfTypeAll<DialogueHandler>()[0].gameObject;
        questWindow = Resources.FindObjectsOfTypeAll<QuestHandler>()[0].gameObject;
        puzzleInvenWindow = Resources.FindObjectsOfTypeAll<DisplayPuzzleInven>()[0].gameObject;
        inventoryWindow = Resources.FindObjectsOfTypeAll<DisplayInventory>()[0].gameObject;
        puzzleWindow = Resources.FindObjectsOfTypeAll<DisplayPlant>()[0].gameObject;
        pauseWindow = Resources.FindObjectsOfTypeAll<PauseHandler>()[0].gameObject;

        // Set Inventory
        puzzleInvenWindow.GetComponent<DisplayPuzzleInven>().CreateDisplay();
        inventoryWindow.GetComponent<DisplayInventory>().CreateDisplay();
    }

    private void Update()
    {
        CheckQuestsInLevel();
        ChangeCurrentQuestInfo();
    }

    #endregion

    #region Public Methods

    public void ShowDialogue(GameObject npc, QuestSO questSO, bool isFinish = false)
    {
        dialogueWindow.SetActive(true);
        dialogueWindow.GetComponent<DialogueHandler>().StartDialogue(npc, questSO, isFinish);
    }

    public void ShowQuest(GameObject npc, QuestSO questSO, QuestStatus questStatus)
    {
        currentNPC = npc;
        currentQuestSO = questSO;

        questWindow.SetActive(true);

        // ChangeWindowColor(questStatus);

        questWindow.GetComponent<QuestHandler>().StartQuest(currentQuestSO);
    }

    public void Inventory()
    {
        inventoryWindow.SetActive(!inventoryWindow.activeSelf);
    }
    public void PuzzleInventory()
    {
        puzzleInvenWindow.SetActive(!puzzleInvenWindow.activeSelf);
    }

    public void Puzzle()
    {
        puzzleWindow.SetActive(!puzzleWindow.activeSelf);
    }

    public void Pause()
    {
        pauseWindow.SetActive(!pauseWindow.activeSelf);
    }

    #endregion

    #region Private Methods

    void CheckQuestsInLevel()
    {
        if (level.mainQuests.Count == 0)
        {
            Debug.Log("Trigger quest for taking player skill");
        }
    }

    void ChangeWindowColor(QuestStatus questStatus)
    {
        if (questStatus == QuestStatus.Completed) questWindow.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#87FF6B", out Color color) ? color : Color.white;

        else if (questStatus == QuestStatus.InProgress) questWindow.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#FFD86B", out Color color2) ? color2 : Color.white;

        else questWindow.GetComponent<Image>().color = ColorUtility.TryParseHtmlString("#FF6B6B", out Color color3) ? color3 : Color.white;
    }

    void ChangeCurrentQuestInfo()
    {
        if (level.mainQuests.Count > 0 && level.mainQuests.Count > questIndex.questID)
        {
            if (level.mainQuests[questIndex.questID].questStatus != QuestStatus.NotStarted)
                questInfoDescription.text = level.mainQuests[questIndex.questID].questName;
        }
        else
            questInfoDescription.text = "No Quest";
    }

    #endregion
}
