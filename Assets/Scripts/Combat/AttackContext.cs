using System.Collections.Generic;
using UnityEngine;

public class AttackContext
{
    public GameObject attacker;
    public Faction attackerFaction;
    public Vector2 origin;
    public Vector2 direction;
    public List<IHitEffect> effects;
}