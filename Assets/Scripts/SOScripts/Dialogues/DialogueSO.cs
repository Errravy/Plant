using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
public class DialogueSO : ScriptableObject
{
  public List<DialogueEntry> dialogueEntries;
  public DialogueType dialogueType;
}

public enum DialogueType
{
  Normal,
  Quest,
}
