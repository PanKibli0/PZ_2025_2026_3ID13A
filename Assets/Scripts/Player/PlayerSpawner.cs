using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform spawnPoint;

    private void Awake()
    {
        Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
    }
}
