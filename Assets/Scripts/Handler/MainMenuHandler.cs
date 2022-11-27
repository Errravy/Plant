using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuHandler : MonoBehaviour
{
  public Button startButton;
  public Button exitButton;

  private void Start()
  {
    startButton.onClick.AddListener(() => NewGame());
    exitButton.onClick.AddListener(() => Exit());
  }

  void NewGame()
  {
    SceneManager.LoadScene("DesaLiekognisi");
  }

  void Exit()
  {
    Application.Quit();
    Debug.Log("Exit");
  }
}
