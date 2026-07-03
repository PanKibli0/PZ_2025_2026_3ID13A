using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Enemy/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public enum EnemyType
    {
        Banan,
        Gabka,
        Zamarzniete,
        Zeschniete,
        Golabek,
        Pomidor,
        Ser,
        Czosnek,
        Piernikonator
    }

    public string enemyName;

    public GameObject enemyPrefab;

    public int maxHealth;

    public List<MovementPhase> movementPhases;
    public List<AttackPhase> attackPhases;

    public int experienceReward = 50;

    public float moneyDropChance = 1f;
    public int moneyDropAmount = 1;

    public EnemyType enemyType;
}