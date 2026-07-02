using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarUI hotbarUI;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerCurrencyUI playerCurrencyUI;

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        PlayerCurrency currency = player.GetComponent<PlayerCurrency>();
        if (currency != null && playerCurrencyUI != null)
        {
            playerCurrencyUI.Init(currency);
        }
        upgradeManager.Init(player);
        RoomController startRoom = spawnPoint.GetComponentInParent<RoomController>();

        player.GetComponent<PlayerSetup>().Init(hotbarUI, playerUI);

        if (startRoom != null)
        {
            cameraController.SetRoom(startRoom.RoomCenter.position);
        }

        EnemySpawner[] spawners = FindObjectsByType<EnemySpawner>(
            FindObjectsSortMode.None);

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.Init(player.transform);
        }

        if (cameraController != null)
            cameraController.Init(player.transform);
    }
}