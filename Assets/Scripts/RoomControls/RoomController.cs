using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public enum RoomState { WaitingForPlayer, InCombat, Cleared}
    private RoomState currentState = RoomState.WaitingForPlayer;

    [SerializeField] private List<DoorController> roomDoors;
    [SerializeField] private GameObject[] enemyPrefabsToSpawn;
    [SerializeField] private Transform[] spawnPoints;
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
        if (currentState == RoomState.WaitingForPlayer && collision.CompareTag("Player"))
        {
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

        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyPrefabsToSpawn.Length; i++)
        {
            //Spawnowanie przeciwników
        }
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

        //Spawn lootu, o ile jest przypisany
        if (lootPrefab != null)
        {
            // Instatniate(lootPrefab, transform.position, Quanternion.identity);
        }
    }
}
