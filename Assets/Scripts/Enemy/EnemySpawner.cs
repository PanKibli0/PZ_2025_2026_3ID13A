using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private Transform player;
    private int activeEnemies;

    public event System.Action OnAllEnemiesDead;
    public void Init(Transform playerTransform)
    {
        player = playerTransform;
    }

    public void SpawnEnemies(List<EnemySpawnEntry> enemiesToSpawn)
    {
        Debug.Log($"Do zrespienia: {enemiesToSpawn.Count}");
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
        if (data.enemyPrefab == null)
        {
            Debug.LogError($"EnemyData '{data.enemyName}' nie ma przypisanego prefabu!");
            return;
        }

        GameObject enemy = Instantiate(data.enemyPrefab, position, Quaternion.identity);

        spawnedEnemies.Add(enemy);

        EnemySetup setup = enemy.GetComponent<EnemySetup>();

        if (setup == null)
        {
            Debug.LogError($"Prefab '{data.enemyPrefab.name}' nie posiada komponentu EnemySetup!");
            Destroy(enemy);
            return;
        }

        setup.Init(data, player, this);

        activeEnemies++;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        spawnedEnemies.Remove(enemy);
        activeEnemies--;

        if (activeEnemies == 0)
            OnAllEnemiesDead?.Invoke();
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
    public void Spawn(List<EnemySpawnEntry> enemies)
    {
        Debug.Log("EnemySpawner Spawn");
        SpawnEnemies(enemies);
    }
}