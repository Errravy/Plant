using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Quest : MonoBehaviour
{
  public TextMeshProUGUI questName;

  public void SetDetail(QuestSO quest)
  {
    questName.text = quest.questName;
  }
}
