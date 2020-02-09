using System;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;
    
    [NonSerialized]
    public bool RuntimeValue;

    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }

    public void OnBeforeSerialize()
    {
    }
}
