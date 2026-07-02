using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickup
{
    [SerializeField] private WeaponData weapon;

    public void OnPickup(PlayerInventory inventory, int currentIndex)
    {
        bool added = inventory.AddWeapon(weapon);

        if (added)
        {
            Debug.Log($"Picked up weapon: {weapon.weaponName}");
            Destroy(gameObject);
            return;
        }

        // TODO UI
        inventory.ReplaceWeaponAt(currentIndex, weapon);
        Destroy(gameObject);
    }
}