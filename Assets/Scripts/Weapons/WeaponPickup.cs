using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickup
{
    [SerializeField] private WeaponData weapon;

    public void OnPickup(PlayerInventory inventory)
    {
        bool added = inventory.AddWeapon(weapon);

        if (added)
        {
            Debug.Log("Picked up weapon: " + weapon.weaponName);
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Inventory full");
        }
    }
}