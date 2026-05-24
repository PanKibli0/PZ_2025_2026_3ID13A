using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private EnemySpawner enemySpawner;

    private void Awake()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        // REWORK: ref from player ROOT 
        Health playerHealth = player.GetComponentInChildren<Health>();
        if (playerHealth != null && playerUI != null)
            playerUI.init(playerHealth);

        if (enemySpawner != null)
            enemySpawner.init(player.transform);
    }
}