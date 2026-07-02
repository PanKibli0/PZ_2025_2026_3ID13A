using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string enemyName;
    public int maxHealth;
    public Color enemyColor; // DEBUG
    public List<MovementPhase> movementPhases;
    public List<AttackPhase> attackPhases;
    public int experienceReward = 50;
    public float moneyDropChance = 1f;
    public int moneyDropAmount = 1;
}