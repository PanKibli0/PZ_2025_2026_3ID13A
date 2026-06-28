using UnityEngine;

[CreateAssetMenu(fileName = "NewShopHealItem", menuName = "Items/Shop Heal Item")]
public class ShopHealItemData : ItemData
{
    public int healAmount = 20;

    public override bool ApplyEffect(PlayerInventory inventory)
    {
        Health health = inventory.GetComponent<Health>();
        if (health != null)
        {
            health.takeHeal(healAmount);
            return true;
        }
        return false;
    }
}
