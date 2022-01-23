using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void Start()
    {
        GameData.Health = 3;
        GameData.Score = 0;
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GoToEasyGameScene()
    {
        GameData.CurrentDiff = GameDiff.Easy;
        SceneManager.LoadScene("Main");
    }

    public void GoToMediumGameScene()
    {
        GameData.CurrentDiff = GameDiff.Medium;
        SceneManager.LoadScene("Main");
    }

    public void GoToHardGameScene()
    {
        GameData.CurrentDiff = GameDiff.Hard;
        SceneManager.LoadScene("Main");
    }

    public void GoToHighscores()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void GoToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void GoToDiffSelect()
    {
        SceneManager.LoadScene("DifficultySelect");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
