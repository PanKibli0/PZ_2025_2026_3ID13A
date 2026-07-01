using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private EnemyMovementHandler movementHandler;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private SpriteRenderer visualRenderer;
    [SerializeField] private EnemyReward reward;

    public void init(EnemyData data, Transform player, EnemySpawner spawner)
    {
        health.setMaxHealth(data.maxHealth);

        health.OnDeath += onDeath;

        reward.Init(data);

        if (visualRenderer != null)
            visualRenderer.color = data.enemyColor;

        movementHandler.init(data.movementPhases, player);
        attackHandler.init(data.attackPhases, player);

        void onDeath()
        {
            reward.GiveRewards();
            spawner.onEnemyDeath(gameObject);
            Destroy(gameObject);
        }
    }
}