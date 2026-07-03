using UnityEngine;

[CreateAssetMenu(menuName = "Items/Key Item")]
public class KeyItemData : ItemData
{
    public override bool ApplyEffect(GameObject player)
    {
        PlayerInventory inventory = player.GetComponentInChildren<PlayerInventory>();

        if (inventory == null)
            return false;

        return inventory.AddItem(this);
    }
}