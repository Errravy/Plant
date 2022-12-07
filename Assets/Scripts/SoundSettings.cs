using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSettings : MonoBehaviour
{
    public Slider soundSlider;
    public AudioMixer audioMixer;
    Camera cam;

    [SerializeField] float volume;

    private void Start()
    {

    }

    private void Update()
    {
        cam = Camera.main;
        GetVolume();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void GetVolume()
    {
        if (soundSlider != null)
            volume = soundSlider.value;
        // audioMixer.GetFloat("Volume", out float volume);
        cam.GetComponent<AudioSource>().volume = volume;
    }
}
