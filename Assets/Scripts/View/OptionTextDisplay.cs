/*  author      : brian tria
 *  date        : april 16, 2020
 *  description : 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionTextDisplay : MonoBehaviour
{
    [SerializeField]
    private MathProblemVariable currentMathProblem;

    [SerializeField]
    private int optionIndex;

    #region LifeCycle

    void OnEnable()
    {
        GameManager.OnDisplayNextChallenge += UpdateOptionText;
    }

    void OnDisable()
    {
        GameManager.OnDisplayNextChallenge -= UpdateOptionText;
    }

    #endregion

    #region Private

    void UpdateOptionText()
    {
        Text optionText = GetComponent<Text>();
        if (optionText == null)
        {
            Debug.LogError("Option text is missing.");
            return;
        }

        MathProblem mathProblem = currentMathProblem.RuntimeValue;

        if (optionIndex == 1)
        {
            optionText.text = mathProblem.option1.ToString("F0");
        }
        else
        {
            optionText.text = mathProblem.option2.ToString("F0");
        }
    }

    #endregion

}
