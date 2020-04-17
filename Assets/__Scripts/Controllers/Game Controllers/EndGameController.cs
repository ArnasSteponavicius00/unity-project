using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using Utils;

public class EndGameController : MonoBehaviour
{
    public GameObject gameOverUI;

    public void EndGameRestart()
    {
        SceneManager.LoadScene(Utils.SceneNames.LEVEL_ONE);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene(Utils.SceneNames.MAIN_MENU);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
