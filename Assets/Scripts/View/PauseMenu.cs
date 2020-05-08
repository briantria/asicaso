using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    #region  Events

    public delegate void PauseMenuAction();
    public static event PauseMenuAction OnQuit;

    #endregion

    public void ResumeGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    public void QuitGame()
    {
        if (OnQuit != null)
        {
            OnQuit();
        }

        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseMenu");
        SceneManager.UnloadSceneAsync("GameHud");
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
    }
}
