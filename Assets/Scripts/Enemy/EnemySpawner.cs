using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;

    private Transform player;
    private int activeEnemies;

    public void Init(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SpawnEnemies(List<EnemySpawnEntry> enemiesToSpawn)
    {
        foreach (var entry in enemiesToSpawn)
        {
            for (int i = 0; i < entry.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                SpawnEnemy(entry.enemyData, spawnPoint.position);
            }
        }
    }

    private void SpawnEnemy(EnemyData data, Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        EnemySetup setup = enemy.GetComponent<EnemySetup>();
        setup.Init(data, player, this);
        activeEnemies++;
    }

    public void OnEnemyDeath()
    {
        activeEnemies--;
        if (activeEnemies == 0)
            EventBus.publishAllEnemiesDefeated();
    }
}
