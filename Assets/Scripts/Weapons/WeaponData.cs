using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Weapons/Weapon Data")]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Info")]
    public string weaponName;
    public AttackData attack;

    [Header("Attack Spawn")]
    public float attackOffset = 0.6f;

    [Header("Visual")]
    public Sprite weaponSprite;
    public Vector2 targetSize = Vector2.one;
}