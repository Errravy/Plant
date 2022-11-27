using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueEntry
{
  public string characterName;
  [TextArea(5, 10)] public List<string> sentences;
}
