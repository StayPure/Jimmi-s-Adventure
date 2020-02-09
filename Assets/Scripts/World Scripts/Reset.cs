using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset : MonoBehaviour
{
    public Inventory playerInvetory;
    public VectorValue[] vectorValues;
    public BoolValue[] boolValues;
    public FloatValue[] floatValues;

    public void ResetGameState()
    {
        playerInvetory.items.Clear();
        playerInvetory.numCoins = 0;
        playerInvetory.numKeys = 0;
        foreach (VectorValue v in vectorValues)
            v.runningValue = v.initalValue;

        foreach (BoolValue b in boolValues)
            b.RuntimeValue = b.initialValue;

        foreach (FloatValue f in floatValues)
            f.RuntimeValue = f.initialValue;
    }
}
