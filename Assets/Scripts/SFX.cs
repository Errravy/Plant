using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public static SFX Instance;

    public AudioClip playerDamagedAudioClip;
    public AudioClip enemyDamagedAudioClip;

    [Space]
    public AudioClip collectAudioClip;
    public AudioClip buttonClickAudioClip;

    [Space]
    public AudioClip dialogueStartAudioClip;
    public AudioClip continueDialogueAudioClip;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}
