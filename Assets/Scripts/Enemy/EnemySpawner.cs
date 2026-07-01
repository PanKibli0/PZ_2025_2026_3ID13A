using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<Transform> spawnPoints;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private Transform player;
    private int activeEnemies;

    public void init(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void spawnEnemies(List<EnemySpawnEntry> enemiesToSpawn)
    {
        foreach (var entry in enemiesToSpawn)
        {
            for (int i = 0; i < entry.count; i++)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                spawnEnemy(entry.enemyData, spawnPoint.position);
            }
        }
    }

    private void spawnEnemy(EnemyData data, Vector2 position)
    {
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        spawnedEnemies.Add(enemy);

        EnemySetup setup = enemy.GetComponent<EnemySetup>();
        setup.init(data, player, this);

        activeEnemies++;
    }

    public void DespawnEnemies()
    {
        foreach (GameObject enemy in spawnedEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }

        spawnedEnemies.Clear();
        activeEnemies = 0;
    }

    public void onEnemyDeath(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);

        activeEnemies--;

        if (activeEnemies == 0)
        {
            EventBus.publishAllEnemiesDefeated();
        }
    }
}