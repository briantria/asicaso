/*  author      : brian tria
 *  date        : april 18, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    #region  Events

    public delegate void MenuAction();
    public static event MenuAction OnPlay;

    #endregion

    // void Update()
    // {
    // TODO: animate
    // }

    #region Public Methods

    public void Play()
    {
        if (OnPlay != null)
        {
            OnPlay();
        }

        SceneManager.LoadSceneAsync("GameHud", LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync("MainMenu");
    }

    #endregion
}
