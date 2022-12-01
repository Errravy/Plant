using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorExit : MonoBehaviour
{
    public string sceneToLoad;
    [HideInInspector] public AudioSource doorAudioSource;
    public AudioClip doorOpenClip;

    private void Start()
    {
        doorAudioSource = GetComponent<AudioSource>();
    }
}
