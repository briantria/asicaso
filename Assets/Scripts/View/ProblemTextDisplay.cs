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

    private Text problemStatement;

    #region LifeCycle

    void OnEnable()
    {
        GameManager.OnGameStart += UpdateProblemStatement;
    }

    void OnDisable()
    {
        GameManager.OnGameStart -= UpdateProblemStatement;
    }

    void Start()
    {
        problemStatement = GetComponent<Text>();
        if (problemStatement == null)
        {
            Debug.LogError("Missing problem text component.");
        }
    }

    #endregion

    #region Private

    void UpdateProblemStatement()
    {
        if (problemStatement == null)
        {
            problemStatement = GetComponent<Text>();
        }

        MathProblem mathProblem = currentMathProblem.RuntimeValue;
        problemStatement.text = mathProblem.statement;
    }

    #endregion
}
