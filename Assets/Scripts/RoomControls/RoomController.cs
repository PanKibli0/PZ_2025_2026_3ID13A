using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public enum RoomState { WaitingForPlayer, InCombat, Cleared}
    private RoomState currentState = RoomState.WaitingForPlayer;

    [SerializeField] private Vector2 roomSize = new Vector2(18f, 10f);

    [SerializeField] private List<DoorController> roomDoors;
    [SerializeField] private GameObject lootPrefab;
    [SerializeField] private ZoneSpawner zoneSpawner;

    private bool roomUnlocked = false;
    public Vector2 RoomSize => roomSize;

    [SerializeField] private Transform roomCenter;
    public Transform RoomCenter => roomCenter;
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

            if (currentState == RoomState.WaitingForPlayer || currentState == RoomState.Cleared)
                StartCombat();
        }
    }

    private void StartCombat()
    {
        Debug.Log($"StartCombat: {name}");
        if (!roomUnlocked)
        {
            currentState = RoomState.InCombat;

            foreach (var door in roomDoors)
                door.CloseDoor();
        }

        zoneSpawner.activateZone();
    }

    private void HandleEnemiesDefeated()
    {
        if (currentState == RoomState.InCombat)
        {
            EndCombat();
        }   
    }
    public void PlayerEnteredRoom()
    {
        Debug.Log($"PlayerEnteredRoom: {name}");
        EventBus.publishOnRoomEntered(RoomCenter.position, RoomSize);

        if (currentState == RoomState.WaitingForPlayer ||
            currentState == RoomState.Cleared)
        {
            StartCombat();
        }
    }

    public void DespawnEnemies()
    {
        zoneSpawner.DespawnEnemies();
    }

    private void EndCombat()
    {
        roomUnlocked = true;
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
