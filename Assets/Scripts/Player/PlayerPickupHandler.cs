using UnityEngine;

public class PlayerPickupHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickup pickup))
        {
            pickup.OnPickup(inventory);
        }
    }
}