using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float cooldown;
    public int damage;

    [SerializeReference]
    [Tooltip("Natychmiastowe efekty")]
    public List<IHitModifier> modifiers;
    // knockback, slow, heal

    // [SerializeReference]
    // [Tooltip("Efekty statusowe")]
    // public List<IHitEffect> effects;
    // burn, poison, stun

    // [SerializeReference]
    // [Tooltip("Eventy po trafieniu")]
    // public List<IOnHitAction> actions;
    // explosion, spawn, chain lightning

    [SerializeReference]
    public AttackPattern attackPattern;
}