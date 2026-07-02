using UnityEngine;
using static EnemyData;

public enum QuestObjectiveType
{
    KillEnemy
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