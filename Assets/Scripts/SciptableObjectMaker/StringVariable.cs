/*  author      : brian tria
 *  date        : april 16, 2020
 *  description : https://unity3d.com/how-to/architect-with-scriptable-objects
 */

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStringVariable", menuName = "ScriptableObject/Variables/String", order = 51)]
public class StringVariable : ScriptableObject
{
    #region Properties
    public String InitValue;

    [NonSerialized] public String RuntimeValue;
    #endregion

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitValue;
    }
}
