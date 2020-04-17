/*  author      : brian tria
 *  date        : april 16, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemTextDisplay : MonoBehaviour
{
    [SerializeField]
    private MathProblemVariable currentMathProblem;

    #region LifeCycle

    void OnEnable()
    {
        GameManager.OnDisplayNextChallenge += UpdateProblemText;
    }

    void OnDisable()
    {
        GameManager.OnDisplayNextChallenge -= UpdateProblemText;
    }

    #endregion

    #region Private

    void UpdateProblemText()
    {
        Text problemStatement = GetComponent<Text>();
        if (problemStatement == null)
        {
            Debug.LogError("Problem statement is missing.");
            return;
        }

        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        problemStatement.text = mathProblem.statement;
    }

    #endregion
}
