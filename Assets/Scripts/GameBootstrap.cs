using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject[] playerPrefabs;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarUI hotbarUI;
    [SerializeField] private UpgradeManager upgradeManager;
    [SerializeField] private PlayerCurrencyUI playerCurrencyUI;
    [SerializeField] private PlayerStatsUI playerStatsUI;

    private const string CharacterKey = "SelectedCharacter";

    private void Start()
    {
        int selectedIndex = PlayerPrefs.GetInt(CharacterKey, 0);

        if (selectedIndex < 0 || selectedIndex >= playerPrefabs.Length)
        {
            selectedIndex = 0;
        }

        GameObject playerToSpawn = playerPrefabs[selectedIndex];
        GameObject player = Instantiate(playerToSpawn, spawnPoint.position, Quaternion.identity);

        PlayerCurrency currency = player.GetComponentInChildren<PlayerCurrency>();
        if (currency != null && playerCurrencyUI != null)
        {
            playerCurrencyUI.Init(currency);
        }
        upgradeManager.Init(player);
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