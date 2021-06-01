using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "FloatDataSO")]
public class FloatDataSO : ScriptableObject
{
    /// <summary>
    /// Value that the FloatData will return.
    /// </summary>
    public float Value
    {
        get {
            return useConstant ? constant : testValue;
        }
    }

    /// <summary>
    /// Use this for quick testing.
    /// </summary>
    public float testValue;

    /// <summary>
    /// Use this for production/actual game value.
    /// </summary>
    public float constant;

    /// <summary>
    /// Whether to use constant or use testValue
    /// </summary>
    public bool useConstant = true;
}
