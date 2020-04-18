/*  author      : brian tria
 *  date        : april 17, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Events

    public delegate void GameAction();
    public static event GameAction OnGameStart;
    public static event GameAction OnDisplayNextChallenge;
    // public static event GameAction OnChooseOption1;
    // public static event GameAction OnChooseOption2;
    public static event GameAction OnAddScore;
    public static event GameAction OnGameOver;

    #endregion

    [SerializeField]
    private MathProblemVariable currentMathProblem;

    private ProblemRandomizer problemRandomizer;
    private int gameLevel;

    void Start()
    {
        if (currentMathProblem == null)
        {
            Debug.LogError("Missing current math problem.");
            return;
        }

        gameLevel = 1;
        problemRandomizer = new ProblemRandomizer();
        problemRandomizer.generateProblemList(gameLevel);
        currentMathProblem.RuntimeValue = problemRandomizer.getNextMathProblem();

        if (OnGameStart != null)
        {
            OnGameStart();
        }

        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    #region Private Methods

    void checkAnswer(float option)
    {
        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        float answer = mathProblem.answer;

        if (option == answer)
        {
            loadNextChallenge();
        }
        else
        {
            Debug.LogError("Wrong answer.");
        }
    }

    void loadNextChallenge()
    {
        MathProblem mathProblem = problemRandomizer.getNextMathProblem();

        if (String.IsNullOrEmpty(mathProblem.statement))
        {
            loadNextLevel();
            return;
        }

        currentMathProblem.RuntimeValue = mathProblem;
        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    void loadNextLevel()
    {
        gameLevel++;
        problemRandomizer.resetProblemList();
        problemRandomizer.generateProblemList(gameLevel);
        currentMathProblem.RuntimeValue = problemRandomizer.getNextMathProblem();

        if (OnDisplayNextChallenge != null)
        {
            OnDisplayNextChallenge();
        }
    }

    #endregion

    #region Public Methods

    public void ChooseOption(int optionIndex)
    {
        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        //float answer = mathProblem.answer;

        // 0: padding; 1: option1; 2: option2
        float[] options = new float[3];
        options[1] = mathProblem.option1;
        options[2] = mathProblem.option2;

        checkAnswer(options[optionIndex]);
    }

    #endregion
}
