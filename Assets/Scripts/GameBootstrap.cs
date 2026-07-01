using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarUI hotbarUI;
    [SerializeField] private UpgradeManager upgradeManager;

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        upgradeManager.Init(player);
        RoomController startRoom = spawnPoint.GetComponentInParent<RoomController>();

        if (startRoom != null)
        {
            cameraController.SetRoom(startRoom.RoomCenter.position);
        }
        // REWORK: ref from player ROOT 
        Health playerHealth = player.GetComponentInChildren<Health>();
        if (playerHealth != null && playerUI != null)
            playerUI.init(playerHealth);
        
        EnemySpawner[] spawners = FindObjectsByType<EnemySpawner>(
            FindObjectsSortMode.None);

        foreach (EnemySpawner spawner in spawners)
        {
            spawner.init(player.transform);
        }

        if (cameraController != null)
            cameraController.Init(player.transform);

        PlayerWeaponHandler weaponHandler = player.GetComponentInChildren<PlayerWeaponHandler>();
        weaponHandler.SetHotbar(hotbarUI);
    }
}