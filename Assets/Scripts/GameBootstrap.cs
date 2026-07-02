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
    [SerializeField] private PlayerStatsUI playerStatsUI;
    [SerializeField] private QuestManager questManager;


    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        PlayerCurrency currency = player.GetComponentInChildren<PlayerCurrency>();
        if (currency != null && playerCurrencyUI != null)
        {
            playerCurrencyUI.Init(currency);
        }
        upgradeManager.Init(player);
        questManager.Init(player);
        RoomController startRoom = spawnPoint.GetComponentInParent<RoomController>();

        player.GetComponent<PlayerSetup>().Init(hotbarUI, playerUI);
        playerStatsUI.Init(player);
        PlayerInputController input = player.GetComponentInChildren<PlayerInputController>();

        input.SetStatsUI(playerStatsUI);
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