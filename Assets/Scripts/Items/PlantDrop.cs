using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantDrop : MonoBehaviour
{
    [SerializeField] PlantSO plant;

    public void GetPlant()
    {
        PlantSave.Instance.plants.Add(plant);
        Destroy(gameObject);
    }
}
