using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AttackData
{
    [Header("Attack Info")]
    public int damage;
    public float cooldown;

    [Header("Attack Effects")]
    [SerializeReference]
    public List<IHitEffect> effects;

    [Header("Attack Pattern")]
    [SerializeReference]
    public AttackPattern pattern;

    public void Execute(HitContext context)
    {
        pattern.Execute(context);
    }

    public HitContext CreateContext(Unit attacker, Vector2 origin, Vector2 direction)
    {
        return new HitContext(attacker, origin, direction, damage, effects);
    }
}