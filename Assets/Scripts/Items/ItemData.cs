using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;

    public abstract bool ApplyEffect(PlayerInventory inventory);
}
