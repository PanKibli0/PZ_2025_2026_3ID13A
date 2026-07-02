using UnityEngine;
using static EnemyData;

public enum QuestObjectiveType
{
    KillEnemy,         // zabij konkretny typ przeciwnika
    KillAnyEnemy,      // zabij dowolnego przeciwnika
    CollectMoney,      // zbierz okreœlon¹ iloœæ pieniêdzy
    ReachLevel,        // osi¹gnij poziom
    BuyItems,          // kup przedmioty
    ClearRooms         // wyczyœæ pokoje
}

[CreateAssetMenu(menuName = "Quest/Quest Data")]
public class QuestData : ScriptableObject
{
    public string questName;
    [TextArea]
    public string description;

    public QuestObjectiveType objectiveType;

    public EnemyType enemyType;
    public int requiredAmount;

    public int moneyReward;
    public int experienceReward;
}