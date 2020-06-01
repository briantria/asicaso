/*  author      : brian tria
 *  date        : april 16, 2020
 *  description : 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProblemRandomizer
{
    const int MaxProblemPerLevel = 10;
    const int LevelRandomRange = 3;
    private List<MathProblem> problemList;

    #region Public

    public ProblemRandomizer()
    {
        problemList = new List<MathProblem>();
    }

    public void ResetProblemList()
    {
        problemList = new List<MathProblem>();
    }

    public void GenerateProblemList(int level)
    {
        int mathOperator = Random.Range(1, 4);
        int item1 = Random.Range(level, level + LevelRandomRange);
        int item2 = Random.Range(level, level + LevelRandomRange);
        MathProblem mathProblem = new MathProblem();

        switch (mathOperator)
        {
            case 1:
                {
                    mathProblem.statement = item1 + " + " + item2;
                    mathProblem.answer = item1 + item2;
                    break;
                }
            case 2:
                {
                    mathProblem.statement = item1 + " - " + item2;
                    mathProblem.answer = item1 - item2;
                    break;
                }
            case 3:
                {
                    mathProblem.statement = item1 + " x " + item2;
                    mathProblem.answer = item1 * item2;
                    break;
                }
            default:
                {
                    if (item2 == 0 || item1 % item2 > 0)
                    {
                        GenerateProblemList(level);
                        return;
                    }

                    mathProblem.statement = item1 + " ÷ " + item2;
                    mathProblem.answer = item1 / item2;
                    break;
                }
        }

        int offset = 0;
        while (offset == 0)
        {
            offset = Random.Range(-2, 2);
        }

        if (Random.value > 0.5f)
        {
            mathProblem.option1 = mathProblem.answer;
            mathProblem.option2 = mathProblem.answer + offset;
        }
        else
        {
            mathProblem.option1 = mathProblem.answer + offset;
            mathProblem.option2 = mathProblem.answer;
        }

        if (problemList.Contains(mathProblem))
        {
            GenerateProblemList(level);
            return;
        }

        problemList.Add(mathProblem);

        if (problemList.Count < MaxProblemPerLevel)
        {
            GenerateProblemList(level);
        }
    }

    public MathProblem GetNextMathProblem()
    {
        MathProblem mathProblem = new MathProblem();

        if (problemList.Count == 0)
        {
            return mathProblem;
        }

        mathProblem = problemList[0];
        problemList.RemoveAt(0);

        return mathProblem;
    }

    #endregion

    #region Private

    MathProblem GetMathProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        // TODO: rendom operator

        return mathProblem;
    }

    MathProblem GenerateAdditionProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        return mathProblem;
    }

    MathProblem GenerateSubtractionProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        return mathProblem;
    }

    MathProblem GenerateMultiplicationProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        return mathProblem;
    }

    MathProblem GenerateDivisionProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        return mathProblem;
    }

    #endregion
}