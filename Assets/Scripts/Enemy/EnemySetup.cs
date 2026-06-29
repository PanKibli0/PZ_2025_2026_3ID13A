using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] private EnemyUnit unit;
    [SerializeField] private EnemyMovementHandler movementHandler;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private SpriteRenderer visualRenderer;

    public void Init(EnemyData data, Transform player, EnemySpawner spawner)
    {
        unit.faction.factionType = FactionType.Enemy;
        unit.health.Init(data.maxHealth);
        unit.health.OnDeath += spawner.OnEnemyDeath;
        unit.moveHandler = movementHandler;
        unit.attackHandler = attackHandler;

        if (visualRenderer != null)
            visualRenderer.color = data.enemyColor;

        movementHandler.Init(data.movementPhases, player);
        attackHandler.Init(data.attackPhases, player);
    }
}