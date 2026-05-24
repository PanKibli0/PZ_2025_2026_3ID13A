using UnityEngine;

[System.Serializable]
public class MovementPhase
{
    [SerializeReference] public ICondition condition;
    [SerializeReference] public IEnemyMove movement;
    public float speed;
}