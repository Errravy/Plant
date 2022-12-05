using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantSave : MonoBehaviour
{
    public static PlantSave Instance;

    public List<PlantSO> plants = new List<PlantSO>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public int GetItemCount(PlantSO plantSO)
    {
        int count = 0;

        foreach (PlantSO plant in plants)
        {
            if (plant == plantSO)
                count++;
        }

        return count;
    }
}
