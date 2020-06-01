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
        if (problemList == null)
        {
            problemList = new List<MathProblem>();
        }

        problemList.Add(GenerateMathProblem(level));
        if (problemList.Count < MaxProblemPerLevel)
        {
            GenerateProblemList(level);
        }

        // ---------
        return;

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

    MathProblem GenerateMathProblem(int level)
    {
        // addition only
        int mathOperator = 1;

        // all operations unlocked
        if (level > 30)
        {
            mathOperator = Random.Range(1, 4);
        }
        // unlock multiplication
        else if (level > 20)
        {
            mathOperator = Random.Range(1, 3);
        }
        // unlock subtraction
        else if (level > 10)
        {
            mathOperator = Random.Range(1, 2);
        }


        switch (mathOperator)
        {
            case 1: return GenerateAdditionProblem(level);
            case 2: return GenerateSubtractionProblem(level);
            case 3: return GenerateMultiplicationProblem(level);
            default: return GenerateDivisionProblem(level);
        }
    }

    MathProblem AssignOptions(int level, MathProblem mathProblem)
    {
        int offset = 0;
        while (offset == 0)
        {
            // as the level goes up, the difference between the options decreases
            // the idea is to gradually make the correct answer less obvious
            offset = (10 / level) + 2;
            offset = Random.Range(-offset, offset);
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

        return mathProblem;
    }

    Vector2 GenerateGivens(int level)
    {
        int item1 = Random.Range(level, level + LevelRandomRange);

        // let's try to slow down difficulty progression
        int halfLevel = level / 2;
        int item2 = Random.Range(halfLevel, halfLevel + LevelRandomRange);

        return new Vector2(item1, item2);
    }

    MathProblem GenerateAdditionProblem(int level)
    {
        Vector2 givens = GenerateGivens(level);
        MathProblem mathProblem = new MathProblem();
        mathProblem.statement = givens.x + " + " + givens.y;
        mathProblem.answer = givens.x + givens.y;
        mathProblem = AssignOptions(level, mathProblem);

        return mathProblem;
    }

    MathProblem GenerateSubtractionProblem(int level)
    {
        Vector2 givens = GenerateGivens(level);
        MathProblem mathProblem = new MathProblem();
        mathProblem.statement = givens.x + " - " + givens.y;
        mathProblem.answer = givens.x - givens.y;
        mathProblem = AssignOptions(level, mathProblem);

        return mathProblem;
    }

    MathProblem GenerateMultiplicationProblem(int level)
    {
        Vector2 givens = GenerateGivens(level);
        MathProblem mathProblem = new MathProblem();
        mathProblem.statement = givens.x + " x " + givens.y;
        mathProblem.answer = givens.x * givens.y;
        mathProblem = AssignOptions(level, mathProblem);

        return mathProblem;
    }

    MathProblem GenerateDivisionProblem(int level)
    {
        MathProblem mathProblem = new MathProblem();

        return mathProblem;
    }

    #endregion
}