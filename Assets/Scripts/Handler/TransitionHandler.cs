using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionHandler : MonoBehaviour
{
  public GameObject fadeOutPrefab;

  private void Start()
  {
    if (fadeOutPrefab != null)
      Destroy(Instantiate(fadeOutPrefab, Vector3.zero, Quaternion.identity), 1);
  }

  public IEnumerator Transition(GameObject currentDoor)
  {
    PlayerPrefs.SetString("SceneToLoad", currentDoor.GetComponent<DoorExit>().sceneToLoad);
    PlayerPrefs.SetString("SceneLoadedFrom", SceneManager.GetActiveScene().name);

    AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(currentDoor.GetComponent<DoorExit>().sceneToLoad);

    while (!asyncOperation.isDone)
    {
      yield return null;
    }
  }
}
