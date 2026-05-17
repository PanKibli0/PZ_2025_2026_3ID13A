using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public float cooldown;

    [SerializeReference]
    public List<IHitEffect> baseEffects;

    [SerializeReference]
    public AttackPattern attackPattern;
}