using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject[] drop;
    [SerializeField] GameObject[] enemy;
    [SerializeField] Sprite chestState;
    [SerializeField] Transform dropArea;
    [SerializeField] float dropAreaRadius;
    bool dropped = false;

    private void Start()
    {

    }
    private void Update()
    {
        if (areSame(enemy) && !dropped)
        {
            dropped = true;

            GetComponent<SpriteRenderer>().sprite = chestState;

            // Random drop item position inside dropArea
            Vector3 dropPosition = new Vector2(Random.Range(-dropAreaRadius, dropAreaRadius), Random.Range(-dropAreaRadius, dropAreaRadius));
            // Instantiate drop item
            Instantiate(drop[Random.Range(0, drop.Length)], dropArea.position + dropPosition, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (dropArea == null)
            return;

        Gizmos.DrawWireSphere(dropArea.position, dropAreaRadius);
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
