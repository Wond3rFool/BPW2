using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public static void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }
    public static void MainMenu()
    {
        SceneManager.LoadScene("Start");
    }

    public static void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
    public static void EndGame()
    {
        Application.Quit();
    }
}
