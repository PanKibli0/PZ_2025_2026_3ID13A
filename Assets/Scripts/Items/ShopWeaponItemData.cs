using UnityEngine;

[CreateAssetMenu(fileName = "NewShopWeaponItemData", menuName = "Items/New Shop Weapon Item")]
public class ShopWeaponItemData : ItemData
{
    public WeaponData weaponToGive;

    public override bool ApplyEffect(GameObject player)
    {
        PlayerInventory inventory = player.GetComponentInChildren<PlayerInventory>();
        bool success = inventory.AddWeapon(weaponToGive);
        if (success)
        {
            Debug.Log($"Dodano broñ {weaponToGive.name} do eq");
        }
        else
        {
            Debug.Log("Brak miejsca na broñ");
        }
        return success;
    }
}
