using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(IHitEffect), true)]
public class IHitEffectDrawer : BaseSerializeReferenceDrawer
{
    protected override Type[] getTypes()
    {
        return new Type[]
        {
            typeof(KnockbackHitEffect)
        };
    }
}