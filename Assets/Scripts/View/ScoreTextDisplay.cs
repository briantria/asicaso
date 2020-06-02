/*  author      : brian tria
 *  date        : june 02, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextDisplay : MonoBehaviour
{
    #region Properties

    private Text scoreDisplay;
    private int currentScore = 0;

    #endregion

    #region Life Cycle

    void OnEnable()
    {
        GameManager.OnCorrectAnswer += UpdateScore;
    }

    void OnDisable()
    {
        GameManager.OnCorrectAnswer -= UpdateScore;
    }

    void Awake()
    {
        scoreDisplay = GetComponent<Text>();
        if (scoreDisplay == null)
        {
            Debug.Log("Missing score display");
            return;
        }

        scoreDisplay.text = "$ " + currentScore;
    }

    #endregion

    #region Private

    void UpdateScore()
    {
        if (scoreDisplay == null)
        {
            Debug.Log("Missing score display");
            return;
        }

        currentScore += 10;
        scoreDisplay.text = "$ " + currentScore;
    }

    #endregion
}