/* source: https://unity3d.com/how-to/architect-with-scriptable-objects
 */

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStringVariable", menuName = "ScriptableObject/Variables/String", order = 51)]
public class StringVariable : ScriptableObject
{
    #region Properties
    public string InitValue;

    [NonSerialized] public string RuntimeValue;
    #endregion

    public void OnAfterDeserialize()
    {
        RuntimeValue = InitValue;
    }
}
