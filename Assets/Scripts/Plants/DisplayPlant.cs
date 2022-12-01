using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayPlant : MonoBehaviour
{
    public List<PlantSO> plant = new List<PlantSO>();
    [SerializeField] int xStart;
    [SerializeField] int yStart;
    [SerializeField] int numberOfColumn;
    [SerializeField] int xGap;
    [SerializeField] int yGap;
    [HideInInspector] public List<GameObject> plants = new List<GameObject>();
    public PlantSO currentPlant;
    private TextMeshProUGUI text;
    ItemSO icon;
    GameObject icons;
    AudioSource audioSource;
    [SerializeField] AudioClip puzzleCompleteClip;

    public int currentPlantID;
    bool showPuzzle = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        plant = PlantSave.Instance.plants;
        text = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        CreateDisplay();
    }

    private void Update()
    {
        PrintBenefit();
        if (currentPlant != plant[currentPlantID] && plants.Count > 0)
        {
            Destroy(icons);
            for (int i = 0; i < plants.Count; i++)
            {
                Destroy(plants[i]);
                plants.Remove(plants[i]);
            }
        }
        else if (currentPlant != plant[currentPlantID] && plants.Count <= 0)
        {
            Destroy(icons);
            showPuzzle = true;
            currentPlant = plant[currentPlantID];
            icon = plant[currentPlantID].puzzles[0].item;
            for (int i = 0; i < currentPlant.puzzles.Count; i++)
            {
                plants.Add(null);
            }
        }
        else if (currentPlant == plant[currentPlantID])
        {
            if (showPuzzle)
            {
                showPuzzle = false;
                icons = Instantiate(icon.icon, Vector3.zero, Quaternion.identity, transform);
                icons.GetComponent<RectTransform>().localPosition = new Vector3(0, 225, 0);
                icons.GetComponentInChildren<TextMeshProUGUI>().text = icon.itemName;
            }
            for (int i = 0; i < currentPlant.puzzles.Count; i++)
            {
                if (currentPlant.puzzles[i].finished && plants[i] == null)
                {
                    plants[i] = Instantiate(plant[currentPlantID].puzzles[i].item.graphic, Vector3.zero, Quaternion.identity, transform);
                    plants[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
                }
            }
        }

    }
    bool CheckFinishPuzzle()
    {
        for (int i = 0; i < currentPlant.puzzles.Count; i++)
        {
            if (!currentPlant.puzzles[i].finished)
                return false;
        }
        return true;
    }
    void PrintBenefit()
    {
        if (CheckFinishPuzzle())
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(puzzleCompleteClip);
            }

            text.text = "Manfaat: \n1, " + currentPlant.puzzles[0].item.description +
                        "\n2, " + currentPlant.puzzles[1].item.description +
                        "\n3, " + currentPlant.puzzles[2].item.description;

            currentPlant.isFinished = true;
        }
        else
            text.text = "";
    }
    void CreateDisplay()
    {
        icon = plant[currentPlantID].puzzles[0].item;
        icons = Instantiate(icon.icon, Vector3.zero, Quaternion.identity, transform);
        icons.GetComponent<RectTransform>().localPosition = new Vector3(0, 225, 0);
        icons.GetComponentInChildren<TextMeshProUGUI>().text = icon.itemName;
        showPuzzle = false;
        for (int i = 0; i < plant[currentPlantID].puzzles.Count; i++)
        {
            currentPlant = plant[currentPlantID];
            plants.Add(null);
            if (currentPlant.puzzles[i].finished)
            {
                plants[i] = Instantiate(plant[currentPlantID].puzzles[i].item.graphic, Vector3.zero, Quaternion.identity, transform);
                plants[i].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
        }
    }
    Vector3 GetPosition(int i)
    {
        return new Vector3((xStart + xGap * (i % numberOfColumn)), (yStart + -yGap * (i / numberOfColumn)), 0);
    }

    public void GetPlantNumber(int i)
    {
        currentPlantID = i;
    }
}
