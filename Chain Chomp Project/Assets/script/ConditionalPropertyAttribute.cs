using System;
using UnityEngine;
[AttributeUsage(AttributeTargets.Field)]
public class ConditionalPropertyAttribute : PropertyAttribute
{
    public string propertyToCheck;
    public object compareValue;
    public ConditionalPropertyAttribute(string propertyToCheck, object compareValue)
    {
        this.propertyToCheck = propertyToCheck;
        this.compareValue = compareValue;
    }
}

