using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public enum RoomState { WaitingForPlayer, InCombat, Cleared }
    private RoomState currentState = RoomState.WaitingForPlayer;

    [SerializeField] private Vector2 roomSize = new Vector2(18f, 10f);
    [SerializeField] private List<DoorController> roomDoors;
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private ZoneSpawner zoneSpawner;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject keyPickupPrefab;
    [SerializeField] private Transform keySpawnPoint;
    [SerializeField] private Transform roomCenter;

    [SerializeField] private bool autoStartCombat = true;

    private bool roomUnlocked = false;
    public Vector2 RoomSize => roomSize;
    public Transform RoomCenter => roomCenter;

    private void Awake()
    {
        if (enemySpawner == null)
            enemySpawner = GetComponentInChildren<EnemySpawner>();

        if (enemySpawner != null)
            enemySpawner.OnAllEnemiesDead += HandleEnemiesDefeated;
    }

    private void OnDisable()
    {
        if (enemySpawner != null)
            enemySpawner.OnAllEnemiesDead -= HandleEnemiesDefeated;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
         if (autoStartCombat && (currentState == RoomState.WaitingForPlayer || currentState == RoomState.Cleared))
            {
                StartCombat();
            }
        }
    }

    public void StartEventCombat()
    {
        if (currentState == RoomState.WaitingForPlayer)
        {
            StartCombat();
        }
    }

    private void StartCombat()
    {
        if (zoneSpawner == null)
        {
            roomUnlocked = true;
            currentState = RoomState.Cleared;
            return;
        }

        if (!roomUnlocked)
        {
            currentState = RoomState.InCombat;
            foreach (var door in roomDoors)
                door.CloseDoor();
        }

        zoneSpawner.activateZoneManual();
    }

    private void HandleEnemiesDefeated()
    {
        if (currentState != RoomState.InCombat)
            return;

        EndCombat();
        SpawnKey();
    }

    private void SpawnKey()
    {
        if (keyPickupPrefab == null) return;

        Vector3 pos = keySpawnPoint != null ? keySpawnPoint.position : transform.position;
        Instantiate(keyPickupPrefab, pos, Quaternion.identity);
        Debug.Log("Klucz zrespiony na scenie!");
    }

    private void EndCombat()
    {
        roomUnlocked = true;
        currentState = RoomState.Cleared;

        foreach (var door in roomDoors)
            door.OpenDoor();

        EventBus.PublishRoomCleared();
    }
    public void PlayerEnteredRoom()
    {
        EventBus.publishOnRoomEntered(RoomCenter.position, RoomSize);

        if (autoStartCombat && (currentState == RoomState.WaitingForPlayer || currentState == RoomState.Cleared))
        {
            StartCombat();
        }
    }
    public void DespawnEnemies()
    {
        if (zoneSpawner != null)
        {
            zoneSpawner.DespawnEnemies();
        }
    }
}