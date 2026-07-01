using UnityEngine;

public class GameBootstrap : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private PlayerUI playerUI;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private HotbarUI hotbarUI;

    private void Start()
    {
        GameObject player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        RoomController startRoom = spawnPoint.GetComponentInParent<RoomController>();

        if (startRoom != null)
        {
            cameraController.SetRoom(startRoom.RoomCenter.position);
        }
        // REWORK: ref from player ROOT 
        Health playerHealth = player.GetComponentInChildren<Health>();
        if (playerHealth != null && playerUI != null)
            playerUI.init(playerHealth);

        if (enemySpawner != null)
            enemySpawner.init(player.transform);

        if (cameraController != null)
            cameraController.Init(player.transform);

        PlayerWeaponHandler weaponHandler = player.GetComponentInChildren<PlayerWeaponHandler>();
        weaponHandler.SetHotbar(hotbarUI);
    }
}