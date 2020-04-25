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
    #region Public Methods

    public void Play()
    {
        SceneManager.LoadSceneAsync("QuickArithmetic");
    }

    #endregion
}
