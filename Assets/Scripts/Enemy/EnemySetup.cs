using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] private EnemyUnit unit;
    [SerializeField] private EnemyMovementHandler movementHandler;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private EnemyReward reward;
    private EnemyData data;

    private EnemySpawner spawner;

    public void Init(EnemyData data, Transform player, EnemySpawner spawner)
    {
        this.data = data;

        unit.faction.factionType = FactionType.Enemy;
        unit.health.Init(data.maxHealth);
        unit.health.OnDeath += HandleDeath;
        unit.moveHandler = movementHandler;
        unit.attackHandler = attackHandler;

        reward.Init(data);
        this.spawner = spawner;

        movementHandler.Init(data.movementPhases, player);
        attackHandler.Init(data.attackPhases, player);
    }

    private void HandleDeath()
    {
        unit.health.OnDeath -= HandleDeath;
        reward.GiveRewards();
        EventBus.PublishEnemyKilled(data.enemyType);

        spawner.OnEnemyDeath(gameObject);
    }
}