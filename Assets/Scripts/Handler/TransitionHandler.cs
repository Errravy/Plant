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
        DoorExit doorExit = currentDoor.GetComponent<DoorExit>();

        PlayerPrefs.SetString("SceneToLoad", doorExit.sceneToLoad);
        PlayerPrefs.SetString("SceneLoadedFrom", SceneManager.GetActiveScene().name);

        StartCoroutine(PlayChangeSceneSound(doorExit.doorAudioSource, doorExit.doorOpenClip));

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(doorExit.sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator PlayChangeSceneSound(AudioSource audioSource, AudioClip audio)
    {
        audioSource.PlayOneShot(audio);
        yield return new WaitForSeconds(1);
    }
}
