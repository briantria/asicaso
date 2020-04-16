/*  author      : brian tria
 *  date        : april 16, 2020
 *  description : 
 */

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GamePlayTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void GamePlayTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator GenerateProblemList()
        {
            ProblemRandomizer problemRandomizer = new ProblemRandomizer();
            problemRandomizer.generateProblemList(1);

            int mathProblemCount = 0;
            MathProblem mathProblem = problemRandomizer.getNextMathProblem();
            Assert.IsNotEmpty(mathProblem.statement, "initial math problem statement should not be empty.");

            while (!String.IsNullOrEmpty(mathProblem.statement))
            {
                mathProblemCount++;
                TestContext.WriteLine(mathProblem.statement + " = [a] " + mathProblem.option1 + ", [b] " + mathProblem.option2);
                Assert.AreNotEqual(mathProblem.option1, mathProblem.option2, "options should never be equal.");
                mathProblem = problemRandomizer.getNextMathProblem();
            }

            Assert.AreEqual(mathProblemCount, 10, "problem list count should be 10.");

            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
