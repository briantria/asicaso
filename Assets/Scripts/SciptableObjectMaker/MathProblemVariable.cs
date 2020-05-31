/* author: Brian Tria
 * created: Apr 17, 2020
 * description: 
 */

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStringVariable", menuName = "ScriptableObject/Variables/MathProblem", order = 51)]
public class MathProblemVariable : ScriptableObject
{
    #region Properties
    public MathProblem InitValue;

    [NonSerialized] public MathProblem RuntimeValue;
    #endregion

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitValue;
    }
}
