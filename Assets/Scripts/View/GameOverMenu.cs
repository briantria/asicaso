/*  author      : brian tria
 *  date        : june 7, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public delegate void GameOverMenuAction();
    public static event GameOverMenuAction OnQuit;
    public static event GameOverMenuAction OnRetry;

    public void RetryGame()
    {
        if (OnRetry != null)
        {
            OnRetry();
        }

        // Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("GameOver");
    }

    public void QuitGame()
    {
        if (OnQuit != null)
        {
            OnQuit();
        }

        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("GameOver");
        SceneManager.UnloadSceneAsync("GameHud");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
}