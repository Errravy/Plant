using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEntry : MonoBehaviour
{
  Transform player;
  public string sceneLoadedFrom;

  private void Start()
  {
    player = FindObjectOfType<Player>().transform;

    if (PlayerPrefs.GetString("SceneToLoad") == SceneManager.GetActiveScene().name && PlayerPrefs.GetString("SceneLoadedFrom") == sceneLoadedFrom)
    {
      player.position = transform.position;
      player.eulerAngles = transform.eulerAngles;

      PlayerPrefs.DeleteKey("SceneToLoad");
      PlayerPrefs.DeleteKey("SceneLoadedFrom");
    }
  }
}
