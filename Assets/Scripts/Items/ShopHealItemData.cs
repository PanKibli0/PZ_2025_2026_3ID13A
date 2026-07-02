using UnityEngine;

[CreateAssetMenu(fileName = "NewShopHealItem", menuName = "Items/Shop Heal Item")]
public class ShopHealItemData : ItemData
{
    public int healAmount = 20;

    public override bool ApplyEffect(GameObject player)
    {
        PlayerUnit unit = player.GetComponent<PlayerUnit>();
        if (unit != null)
        {
            unit.health.TakeHeal(healAmount);
            return true;
        }
        return false;
    }
}