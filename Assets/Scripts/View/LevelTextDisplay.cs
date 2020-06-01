/*  author      : brian tria
 *  date        : june 1, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTextDisplay : MonoBehaviour
{
    #region Properties

    [SerializeField]
    private IntVariable currentLevel;

    private Text levelDisplay;

    #endregion

    #region LifeCycle

    void OnEnable()
    {
        GameManager.OnLevelUp += UpdateLevel;
    }

    void OnDisable()
    {
        GameManager.OnLevelUp -= UpdateLevel;
    }

    void Awake()
    {
        levelDisplay = GetComponent<Text>();
    }

    #endregion

    #region Private

    void UpdateLevel()
    {
        if (levelDisplay == null)
        {
            Debug.Log("Missing level display text.");
            return;
        }

        levelDisplay.text = "LVL " + currentLevel.RuntimeValue;
    }

    #endregion
}