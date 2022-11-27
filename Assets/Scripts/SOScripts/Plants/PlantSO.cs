using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Plant", fileName = "New Plant")]
public class PlantSO : ScriptableObject
{
  public List<PuzzleBox> puzzles = new List<PuzzleBox>();
}

[System.Serializable]
public class PuzzleBox
{
  public PlantType plant;
  public ItemSO item;
  public bool finished;
  public int id;

  public bool InsertPuzzle(PlantType plantName, int getID)
  {
    if (plantName == plant && id == getID && finished)
    {
      return false;
    }
    else if (plantName == plant && id == getID)
    {
      finished = true;
      return true;
    }
    else
    {
      return true;
    }
  }
}

public enum PlantType
{
  Jahe,
  Kunyit,
  JerukNipis,
  Gandarusa,
  JambuBiji,
}
