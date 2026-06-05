using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(IEnemyMove), true)]
public class IEnemyMoveDrawer : BaseSerializeReferenceDrawer
{
    protected override Type[] getTypes()
    {
        return new Type[]
        {
            typeof(IdleMove),
            typeof(TowardsPlayerMove),
            typeof(AwayFromPlayerMove),
            typeof(ArcMove),
            typeof(RandomMove)
        };
    }
}