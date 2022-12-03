using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueHandler : MonoBehaviour
{
    #region Fields

    AudioSource audioSource;

    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueText;
    public Button continueButton;
    [SerializeField] AudioClip typingAudioClip;

    private int dialogueEntriesLength;
    private int sentencesLength;
    private int dialogueIndex;
    private int sentencesIndex;
    private string currentSentence;
    private DialogueSO currentDialogueSO;

    #endregion

    #region MonoBehaviour Methods

    private void Update()
    {
        // auto change the button text to "Close" if only have 1 dialogueEntries and 1 sentences
        if (dialogueEntriesLength == 1 && sentencesLength == 1)
            continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";

        // change the text of the continue button ftom "Continue" to "End" when the dialogue is finished
        if (dialogueIndex == dialogueEntriesLength - 1 && sentencesIndex == sentencesLength - 1)
            continueButton.GetComponentInChildren<TextMeshProUGUI>().text = "Close";
    }

    #endregion

    #region Public Methods

    public void StartDialogue(GameObject npc, QuestSO questSO, bool isFinish)
    {
        audioSource = GetComponent<AudioSource>();

        if (!isFinish) currentDialogueSO = questSO.dialogueSO;
        else currentDialogueSO = questSO.finishDialogueSO;

        // Set the length of the dialogue entries
        dialogueEntriesLength = currentDialogueSO.dialogueEntries.Count;

        // Set the length of the sentences
        sentencesLength = currentDialogueSO.dialogueEntries[0].sentences.Count;

        characterName.text = currentDialogueSO.dialogueEntries[0].characterName;
        StartCoroutine(TypeSentence(currentDialogueSO.dialogueEntries[0].sentences[0]));
        continueButton.onClick.AddListener(() => ContinueDialogue(npc, questSO, isFinish));
    }

    #endregion

    #region Private Methods

    void ContinueDialogue(GameObject npc, QuestSO questSO, bool isFinish)
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.continueDialogueAudioClip, Camera.main.transform.position);

        // Set the length of the sentences
        sentencesLength = currentDialogueSO.dialogueEntries[dialogueIndex].sentences.Count;

        // Check if there are more sentences in the current dialogue entry
        if (sentencesIndex < currentDialogueSO.dialogueEntries[dialogueIndex].sentences.Count - 1)
        {
            StopAllCoroutines();
            sentencesIndex++;
            currentSentence = currentDialogueSO.dialogueEntries[dialogueIndex].sentences[sentencesIndex];
            StartCoroutine(TypeSentence(currentSentence));
        }
        else
        {
            dialogueIndex++;
            sentencesIndex = 0;

            // Check if there are more dialogue entries
            if (dialogueIndex < currentDialogueSO.dialogueEntries.Count)
            {
                StopAllCoroutines();
                characterName.text = currentDialogueSO.dialogueEntries[dialogueIndex].characterName;
                currentSentence = currentDialogueSO.dialogueEntries[dialogueIndex].sentences[sentencesIndex];
                StartCoroutine(TypeSentence(currentSentence));
            }
            else
            {
                ResetDialogue();

                // Check if the dialogue has a quest
                if (currentDialogueSO.dialogueType == DialogueType.Normal)
                    GameManager.Instance.ShowQuest(npc, questSO, questSO.questStatus);

                else if (currentDialogueSO.dialogueType == DialogueType.Quest)
                    GameManager.Instance.ShowQuest(npc, questSO, questSO.questStatus);
                CloseDialogue();
            }
        }
    }

    void CloseDialogue()
    {
        gameObject.SetActive(false);
    }

    void ResetDialogue()
    {
        dialogueIndex = 0;
        sentencesIndex = 0;
        continueButton.onClick.RemoveAllListeners();
    }

    IEnumerator TypeSentence(string sentence)
    {
        audioSource.PlayOneShot(typingAudioClip);

        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }

        audioSource.Stop();
    }

    #endregion
}
