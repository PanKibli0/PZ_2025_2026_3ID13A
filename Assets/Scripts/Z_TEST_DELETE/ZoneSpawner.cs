using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemySpawnEntry
{
    public EnemyData enemyData;
    public int count;
}

public class ZoneSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private List<EnemyData> possibleEnemies;
    [SerializeField] private int totalMinEnemies = 1;
    [SerializeField] private int totalMaxEnemies = 8;
    [SerializeField] private SpriteRenderer zoneVisual;
    [SerializeField] private Collider2D zoneCollider;

    private bool isActive = true;

    private void OnEnable()
    {
        EventBus.OnAllEnemiesDefeated += OnZoneCleared;
    }

    private void OnDisable()
    {
        EventBus.OnAllEnemiesDefeated -= OnZoneCleared;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;
        ActivateZone();
    }

    private void ActivateZone()
    {
        isActive = false;

        if (zoneVisual != null)
            zoneVisual.enabled = false;

        if (zoneCollider != null)
            zoneCollider.enabled = false;

        List<EnemySpawnEntry> selectedEnemies = new List<EnemySpawnEntry>();
        int totalCount = Random.Range(totalMinEnemies, totalMaxEnemies + 1);

        for (int i = 0; i < totalCount; i++)
        {
            EnemyData data = possibleEnemies[Random.Range(0, possibleEnemies.Count)];

            EnemySpawnEntry existing = null;
            foreach (var entry in selectedEnemies)
            {
                if (entry.enemyData == data)
                {
                    existing = entry;
                    break;
                }
            }

            if (existing != null)
                existing.count++;
            else
                selectedEnemies.Add(new EnemySpawnEntry { enemyData = data, count = 1 });
        }

        enemySpawner.SpawnEnemies(selectedEnemies);
    }

    private void OnZoneCleared()
    {
        isActive = true;

        if (zoneVisual != null)
            zoneVisual.enabled = true;

        if (zoneCollider != null)
            zoneCollider.enabled = true;
    }
}