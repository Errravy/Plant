using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] PlantSO[] plants;

    private void Update()
    {
        if (areSame(plants))
        {
            gameObject.SetActive(false);
        }
    }

    bool areSame(PlantSO[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (!arr[i].isFinished)
                return false;
        }
        return true;
    }
}
