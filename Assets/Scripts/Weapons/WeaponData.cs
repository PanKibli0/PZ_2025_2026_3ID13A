using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName;
    public float cooldown;
    public int damage;


    [Header("Attack Spawn")]
    public float attackOffset = 0.6f;

    [Header("Attack Pattern")]
    [SerializeReference]
    public AttackPattern attackPattern;


    [Header("Modifiers")]
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
}
