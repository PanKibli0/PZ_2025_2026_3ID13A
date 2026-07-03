using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] private ItemData keyItem;

    private bool playerInside;
    private GameObject player;

    private void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryPickup();
        }
    }

    private void TryPickup()
    {
        PlayerInventory inventory = player.GetComponentInChildren<PlayerInventory>();

        if (inventory == null)
            return;

        inventory.AddItem(keyItem);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Coœ dotknê³o klucza: {other.gameObject.name} z tagiem: {other.tag}");

        if (!other.CompareTag("Player"))
            return;

        playerInside = true;
        player = other.gameObject;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInside = false;
        player = null;
    }
}