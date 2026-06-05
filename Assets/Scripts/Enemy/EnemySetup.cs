using UnityEngine;

public class EnemySetup : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private EnemyMovementHandler movementHandler;
    [SerializeField] private EnemyAttackHandler attackHandler;
    [SerializeField] private SpriteRenderer visualRenderer;

    public void init(EnemyData data, Transform player, EnemySpawner spawner)
    {
        health.setMaxHealth(data.maxHealth);

        health.OnDeath += onDeath;

        if (visualRenderer != null)
            visualRenderer.color = data.enemyColor;

        movementHandler.init(data.movementPhases, player);
        attackHandler.init(data.attackPhases, player);

        void onDeath()
        {
            spawner.onEnemyDeath();
            Destroy(gameObject);
        }
    }
}