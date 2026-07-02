using UnityEngine;

[CreateAssetMenu(fileName = "NewShopHealItem", menuName = "Items/Shop Heal Item")]
public class ShopHealItemData : ItemData
{
    public int healAmount = 20;

    public override bool ApplyEffect(GameObject player)
    {
        Health health = player.GetComponentInChildren<Health>();
        if (health != null)
        {
            Debug.Log("HealTaken");
            health.TakeHeal(healAmount);
            return true;
        }
        return false;
    }
}
