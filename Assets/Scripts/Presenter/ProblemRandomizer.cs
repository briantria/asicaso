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

    public ProblemRandomizer()
    {
        problemList = new List<MathProblem>();
    }

    public void resetProblemList()
    {
        problemList = new List<MathProblem>();
    }

    public void generateProblemList(int level)
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
                    mathProblem.answer = item1 + item2;
                    break;
                }
            default:
                {
                    if (item2 == 0 || item1 % item2 > 0)
                    {
                        generateProblemList(level);
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
            offset = Random.Range(-5, 5);
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

        problemList.Add(mathProblem);

        if (problemList.Count < MaxProblemPerLevel)
        {
            generateProblemList(level);
        }
    }

    public MathProblem getNextMathProblem()
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
}