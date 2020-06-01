/*  author      : brian tria
 *  date        : april 17, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Events

    public delegate void GameAction();
    public static event GameAction OnGameStart;
    public static event GameAction OnDisplayNextChallenge;
    public static event GameAction OnLevelUp;
    public static event GameAction OnAddScore;
    public static event GameAction OnGameOver;
    public static event GameAction OnCorrectAnswer;
    public static event GameAction OnWrongAnswer;

    #endregion

    [SerializeField]
    private MathProblemVariable currentMathProblem;

    [SerializeField]
    private IntVariable currentLevel;

    private ProblemRandomizer problemRandomizer;
    private int gameLevel;
    private int lifeCount;

    void Start()
    {
        if (currentMathProblem == null)
        {
            Debug.LogError("Missing current math problem.");
            return;
        }

        lifeCount = 3;
        gameLevel = 1;
        problemRandomizer = new ProblemRandomizer();
        problemRandomizer.generateProblemList(gameLevel);
        currentMathProblem.RuntimeValue = problemRandomizer.getNextMathProblem();
        currentLevel.RuntimeValue = gameLevel;

        if (OnGameStart != null)
        {
            OnGameStart();
        }

        if (OnLevelUp != null)
        {
            OnLevelUp();
        }

        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftControl))
        {
            // Debug.Log("option 1");
            ChooseOption(1);
        }


        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightControl))
        {
            // Debug.Log("option 2");
            ChooseOption(2);
        }
    }

    #region Private Methods

    void CheckAnswer(float option)
    {
        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        float answer = mathProblem.answer;

        if (option == answer)
        {
            LoadNextChallenge();

            if (OnCorrectAnswer != null)
            {
                OnCorrectAnswer();
            }
        }
        else
        {
            // Debug.LogError("Wrong answer.");
            DoDamage();
        }
    }

    void LoadNextChallenge()
    {
        MathProblem mathProblem = problemRandomizer.getNextMathProblem();

        if (String.IsNullOrEmpty(mathProblem.statement))
        {
            LoadNextLevel();
            return;
        }

        currentMathProblem.RuntimeValue = mathProblem;
        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    void LoadNextLevel()
    {
        gameLevel++;
        problemRandomizer.resetProblemList();
        problemRandomizer.generateProblemList(gameLevel);
        currentMathProblem.RuntimeValue = problemRandomizer.getNextMathProblem();

        currentLevel.RuntimeValue = gameLevel;

        if (OnLevelUp != null)
        {
            OnLevelUp();
        }

        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    void DoDamage()
    {
        lifeCount--;

        if (lifeCount == 0)
        {
            if (OnGameOver != null)
            {
                OnGameOver();
            }
        }
        else if (OnWrongAnswer != null)
        {
            OnWrongAnswer();
        }
    }

    #endregion

    #region Public Methods

    public void ChooseOption(int optionIndex)
    {
        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        // Debug.Log("choose option " + optionIndex);

        // 0: padding; 1: option1; 2: option2
        float[] options = new float[3];
        options[1] = mathProblem.option1;
        options[2] = mathProblem.option2;

        CheckAnswer(options[optionIndex]);
    }

    public void Pause()
    {
        Time.timeScale = 0;
        SceneManager.LoadSceneAsync("PauseMenu", LoadSceneMode.Additive);
    }

    #endregion
}
