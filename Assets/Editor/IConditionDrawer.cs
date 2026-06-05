using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(ICondition), true)]
public class IConditionDrawer : BaseSerializeReferenceDrawer
{
    protected override Type[] getTypes()
    {
        return new Type[]
        {
            typeof(DistanceCondition),
            typeof(HealthThresholdCondition)
        };
    }
}