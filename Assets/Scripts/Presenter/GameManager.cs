/*  author      : brian tria
 *  date        : april 17, 2020
 *  description : 
 */

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

    #region Public Methods

    public void ChooseOption(int optionIndex)
    {
        switch (optionIndex)
        {
            case 1:
                {
                    // if (OnChooseOption1 != null)
                    // {
                    //     OnChooseOption1();
                    // }

                    break;
                }
            default:
                {
                    // if (OnChooseOption2 != null)
                    // {
                    //     OnChooseOption2();
                    // }

                    break;
                }
        }
    }

    #endregion
}
