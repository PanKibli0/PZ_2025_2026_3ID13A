using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(AttackPattern), true)]
public class AttackPatternDrawer : BaseSerializeReferenceDrawer
{
    protected override Type[] getTypes()
    {
        return new Type[]
        {
            typeof(StabPattern),
            typeof(SwingPattern),
            typeof(ProjectilePattern),
            typeof(ShotgunPattern),
            typeof(AreaPattern)
        };
    }
}