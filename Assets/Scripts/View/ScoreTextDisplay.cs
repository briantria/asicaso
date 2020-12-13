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

    [SerializeField]
    private IntVariable currentScore;

    private Text scoreDisplay;

    #endregion

    #region Life Cycle

    void OnEnable()
    {
        GameManager.OnCorrectAnswer += UpdateScore;
        PauseMenu.OnQuit += ResetScore;
        GameOverMenu.OnRetry += ResetScore;
        GameOverMenu.OnQuit += ResetScore;
    }

    void OnDisable()
    {
        GameManager.OnCorrectAnswer -= UpdateScore;
        PauseMenu.OnQuit -= ResetScore;
        GameOverMenu.OnRetry -= ResetScore;
        GameOverMenu.OnQuit -= ResetScore;
    }

    void Start()
    {
        if (currentScore == null)
        {
            Debug.LogError("Missing current score reference");
            return;
        }

        scoreDisplay = GetComponent<Text>();
        if (scoreDisplay == null)
        {
            Debug.Log("Missing score display");
            return;
        }

        scoreDisplay.text = "$ " + currentScore.RuntimeValue;
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

        currentScore.RuntimeValue += 10;
        scoreDisplay.text = "$ " + currentScore.RuntimeValue;
    }

    void ResetScore()
    {
        currentScore.RuntimeValue = 0;
        scoreDisplay.text = "$ " + currentScore.RuntimeValue;
    }

    #endregion
}