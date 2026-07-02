using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] private EnemyUnit unit;
    [SerializeField] private EnemyMovementHandler movementHandler;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private SpriteRenderer visualRenderer;
    [SerializeField] private EnemyReward reward;

    private EnemySpawner spawner;

    public void Init(EnemyData data, Transform player, EnemySpawner spawner)
    {
        unit.faction.factionType = FactionType.Enemy;
        unit.health.Init(data.maxHealth);
        unit.health.OnDeath += HandleDeath;
        unit.moveHandler = movementHandler;
        unit.attackHandler = attackHandler;

        reward.Init(data);
        this.spawner = spawner;

        if (visualRenderer != null)
            visualRenderer.color = data.enemyColor;

        movementHandler.Init(data.movementPhases, player);
        attackHandler.Init(data.attackPhases, player);
    }

    private void HandleDeath()
    {
        reward.GiveRewards();
        spawner.OnEnemyDeath(gameObject);
    }
}