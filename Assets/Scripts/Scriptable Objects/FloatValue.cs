﻿using System;
using UnityEngine;
[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    
    [NonSerialized]
    public float RuntimeValue;
    
    public void OnAfterDeserialize()
    {
        RuntimeValue = initialValue;
    }
    
    public void OnBeforeSerialize()
    {
    }
}
