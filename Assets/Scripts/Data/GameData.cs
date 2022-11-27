using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class GameData
{
  public int health;
  public int gold;
  public float[] position;
  public string lastScene;

  public GameData(Player player)
  {
    health = PlayerStats.Instance.Health;
    gold = PlayerStats.Instance.Gold;

    position = new float[3];
    position[0] = player.transform.position.x;
    position[1] = player.transform.position.y;
    position[2] = player.transform.position.z;

    lastScene = SceneManager.GetActiveScene().name;
  }
}
