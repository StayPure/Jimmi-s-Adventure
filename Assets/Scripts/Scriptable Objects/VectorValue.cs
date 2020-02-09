using System;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [NonSerialized]
    public Vector2 runningValue;
    
    [Header("Value on starting the game")]
    public Vector2 initalValue;

    public void OnAfterDeserialize()
    {
        runningValue = initalValue;
    }

    public void OnBeforeSerialize()
    {
    }
}
