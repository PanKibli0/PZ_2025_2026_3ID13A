using UnityEngine;
using System.Collections.Generic;

public class HitContext
{
    public Unit attacker;
    public Vector2 origin;
    public Vector2 direction;
    public int damage;
    public List<IHitEffect> effects;

    public HitContext(Unit attacker, Vector2 origin, Vector2 direction, int damage, List<IHitEffect> effects)
    {
        this.attacker = attacker;
        this.origin = origin;
        this.direction = direction;
        this.damage = damage;
        this.effects = effects ?? new List<IHitEffect>();
    }
}