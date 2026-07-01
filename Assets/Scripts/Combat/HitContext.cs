using UnityEngine;
using System.Collections.Generic;

public class HitContext
{
    public GameObject attacker;
    public Faction attackerFaction;
    public Vector2 origin;
    public Vector2 direction;
    public int damage;
    public List<IHitEffect> effects;
    public bool isCritical;

    public HitContext(
    GameObject attacker,
    Faction attackerFaction,
    Vector2 origin,
    Vector2 direction,
    AttackData attack)
    {
        this.attacker = attacker;
        this.attackerFaction = attackerFaction;
        this.origin = origin;
        this.direction = direction;

        this.damage = attack.damage;
        this.effects = attack.effects ?? new List<IHitEffect>();
    }

    // DEBUG - maybe help i Future 
    public HitContext(GameObject attacker, Faction attackerFaction, Vector2 origin, Vector2 direction, 
        int damage, List<IHitEffect> effects = null)
    {
        this.attacker = attacker;
        this.attackerFaction = attackerFaction;
        this.origin = origin;
        this.direction = direction;
        this.damage = damage;
        this.effects = effects ?? new List<IHitEffect>();
    }
}