using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public enum RoomState { WaitingForPlayer, InCombat, Cleared}
    private RoomState currentState = RoomState.WaitingForPlayer;

    [SerializeField] private Vector2 roomSize = new Vector2(18f, 10f);

    [SerializeField] private List<DoorController> roomDoors;
    [SerializeField] private EnemySpawner enemySpawner;
    [SerializeField] private GameObject lootPrefab;

    private void OnEnable()
    {
        EventBus.OnAllEnemiesDefeated += HandleEnemiesDefeated;
    }

    private void OnDisable()
    {
        EventBus.OnAllEnemiesDefeated -= HandleEnemiesDefeated;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            EventBus.publishOnRoomEntered(transform.position, roomSize);

            if (currentState == RoomState.WaitingForPlayer)
                StartCombat();
        }
    }

    private void StartCombat()
    {
        currentState = RoomState.InCombat;
        foreach (var door in roomDoors)
        {
            door.CloseDoor();
        }

        // enemySpawner.SpawnEnemies();
    }

    private void HandleEnemiesDefeated()
    {
        if (currentState == RoomState.InCombat)
        {
            EndCombat();
        }
    }

    private void EndCombat()
    {
        currentState = RoomState.Cleared;

        foreach(var door in roomDoors)
        {
            door.OpenDoor();
        }

        // Spawn lootu, o ile jest przypisany
        if (lootPrefab == null) return;
        
        // Instatniate(lootPrefab, transform.position, Quanternion.identity);
        
    }
}
