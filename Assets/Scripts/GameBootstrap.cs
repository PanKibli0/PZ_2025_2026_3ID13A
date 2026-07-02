using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarUI hotbarUI;

    private void Awake()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);

        player.GetComponent<PlayerSetup>().Init(hotbarUI, playerUI);

        if (enemySpawner != null)
            enemySpawner.Init(player.transform);

        if (cameraController != null)
            cameraController.Init(player.transform);
    }
}
