using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameProgress : MonoBehaviour
{
  public void SaveGame()
  {
    Player player = FindObjectOfType<Player>();

    string path = Application.persistentDataPath + "/player.fun";

    BinaryFormatter formatter = new BinaryFormatter();
    FileStream stream = new FileStream(path, FileMode.Create);
    GameData data = new GameData(player);

    formatter.Serialize(stream, data);
    stream.Close();

    Debug.Log("Save Data");
  }

  public void LoadGame()
  {
    string path = Application.persistentDataPath + "/player.fun";

    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);
      GameData data = formatter.Deserialize(stream) as GameData;

      stream.Close();

      SceneManager.LoadScene(data.lastScene);
      Debug.Log("Load Data");
    }
    else
    {
      Debug.Log("Save file not found in " + path);
    }
  }
}
