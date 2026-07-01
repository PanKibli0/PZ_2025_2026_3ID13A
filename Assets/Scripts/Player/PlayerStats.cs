using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float DamageMultiplier { get; private set; } = 1f;
    public float AttackSpeedMultiplier { get; private set; } = 1f;
    public float MoveSpeedMultiplier { get; private set; } = 1f;

    public float CriticalChance { get; private set; } = 0f;
    public float CriticalDamage { get; private set; } = 2f;

    public float DodgeChance { get; private set; } = 0f;

    public float HealthRegen { get; private set; } = 0f;

    public float Luck { get; private set; } = 0f;

    public void AddDamage(float value) { DamageMultiplier += value; Debug.Log($"DamageMultiplier = {DamageMultiplier}"); }
    public void AddAttackSpeed(float value) => AttackSpeedMultiplier += value;
    public void AddMoveSpeed(float value) => MoveSpeedMultiplier += value;

    public void AddCriticalChance(float value) => CriticalChance += value;
    public void AddCriticalDamage(float value) => CriticalDamage += value;

    public void AddDodgeChance(float value) => DodgeChance += value;

    public void AddHealthRegen(float value) => HealthRegen += value;

    public void AddLuck(float value) => Luck += value;
}