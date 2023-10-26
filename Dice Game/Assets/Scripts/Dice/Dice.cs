using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class Dice
{
    public int currentValue;
    public List<int> values;
    public Dice(float currentValue, List<int> values)
    {
        this.values = values;
    }

    public int Roll()
    {
        int i = Random.Range(0, values.Count);
        currentValue = values[i];
        return currentValue;
    }

    public Dice Clone()
    {
        Dice clone = new Dice(0, values);
        return clone;
    } 
}
