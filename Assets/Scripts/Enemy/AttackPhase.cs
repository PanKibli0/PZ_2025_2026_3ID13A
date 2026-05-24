using System;
using UnityEngine;

[Serializable]
public class AttackPhase
{
    [SerializeReference] public ICondition condition;
    public AttackData attack;
    public float weight = 1f;
}