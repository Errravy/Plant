using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseHandler : MonoBehaviour
{
    public GameObject questPrefab;
    public GameObject questContainer;
    [HideInInspector] public List<QuestSO> mainQuests;

    private void Start()
    {
        mainQuests = GameManager.Instance.level.mainQuests;

        // Loop through all main quests and instantiate them
        foreach (QuestSO quest in mainQuests)
        {
            GameObject questGO = Instantiate(questPrefab, questContainer.transform);

            // Set the details of the quest
            questGO.GetComponent<Quest>().SetDetail(quest);
        }
    }

    private void Update()
    {
        // Get all the quests in the quest container
        var questList = questContainer.GetComponentsInChildren<Quest>();

        if (questList.Length != 0)
        {
            foreach (var questItem in questList)
            {
                // Set the status of the quest
                // If the quest NotStarted, set the interactable to false
                if (mainQuests[questItem.transform.GetSiblingIndex()].questID > GameManager.Instance.questIndex.questID)
                {
                    questItem.GetComponent<Button>().interactable = false;
                }
                else
                    questItem.GetComponent<Button>().interactable = true;
            }
        }
    }

    private void OnDisable()
    {
        // Clear all quests in the container when the pause menu is closed
        foreach (var child in questContainer.GetComponentsInChildren<Quest>())
        {
            Destroy(child);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
