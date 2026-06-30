using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPickupHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    private IPickup currentPickup;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (currentPickup == null) return;

        currentPickup.OnPickup(inventory, weaponHandler.CurrentIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickup pickup))
            currentPickup = pickup;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IPickup pickup) && pickup == currentPickup)
            currentPickup = null;
    }
}