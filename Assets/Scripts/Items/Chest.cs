using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] drop;
    [SerializeField] GameObject[] enemy;
    [SerializeField] Sprite chestState;
    private void Start()
    {

    }
    private void Update()
    {
        if (areSame(enemy))
        {
            GetComponent<SpriteRenderer>().sprite = chestState;
        }
    }
    bool areSame(GameObject[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            if (arr[i] != null)
                return false;
        }
        return true;
    }
}
