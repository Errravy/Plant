using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPHandler : MonoBehaviour
{
    Slider healthBar;

    private void Start()
    {
        healthBar = GetComponent<Slider>();
    }
    private void Update()
    {
        healthBar.value = FindObjectOfType<Player>().health;
    }
}
