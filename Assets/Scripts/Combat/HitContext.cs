using System.Collections.Generic;
using UnityEngine;

public class HitContext
{
    public GameObject attacker;
    public Faction attackerFaction;
    public Vector2 origin;
    public Vector2 direction;
    public int damage;
    public List<IHitModifier> modifiers;

    public HitContext(GameObject attacker, Faction attackerFaction, Vector2 origin, Vector2 direction, WeaponData weapon)
    {
        this.attacker = attacker;
        this.attackerFaction = attackerFaction;
        this.origin = origin;
        this.direction = direction;
        this.damage = weapon.damage;
        this.modifiers = weapon.modifiers;
    }

    // DEBUG - maybe help i Future 
    public HitContext(GameObject attacker, Faction attackerFaction, Vector2 origin, Vector2 direction, 
        int damage, List<IHitModifier> modifiers = null)
    {
        this.attacker = attacker;
        this.attackerFaction = attackerFaction;
        this.origin = origin;
        this.direction = direction;
        this.damage = damage;
        this.modifiers = modifiers;
    }
}