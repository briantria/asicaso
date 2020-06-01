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

        // for debugging
        // mathOperator = 4;

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
        /*
        Division
        1. Randomize 2nd given
        2. Randomize answer
        3. 1st Given = 2nd given x answer
        */

        Vector2 givens = GenerateGivens(level);
        int trialCount = 0;
        while (givens.y == 0 && trialCount > 10)
        {
            GenerateGivens(level);
            trialCount++;
        }

        if (givens.y == 0)
        {
            givens.y = level;
        }

        int item1 = (int)(givens.x * givens.y);
        MathProblem mathProblem = new MathProblem();
        mathProblem.statement = item1 + " ÷ " + givens.y;
        mathProblem.answer = givens.x;
        mathProblem = AssignOptions(level, mathProblem);

        return mathProblem;
    }

    #endregion
}