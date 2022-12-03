using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDrop : MonoBehaviour
{
    [SerializeField] PlantSO plant;

    public void GetPlant()
    {
        AudioSource.PlayClipAtPoint(SFX.Instance.collectAudioClip, transform.position);

        PlantSave.Instance.plants.Add(plant);
        Destroy(gameObject);
    }
}
