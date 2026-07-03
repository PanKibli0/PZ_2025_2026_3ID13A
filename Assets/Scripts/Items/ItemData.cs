using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    public string itemName;
    public string description;
    public Sprite icon;

    [SerializeField] protected StatusEffectType statusEffect = StatusEffectType.None;

    [SerializeField] protected float effectDuration = 5f;
    [SerializeField] protected int effectDamage = 2;
    [SerializeField] protected float effectMultiplier = 0.5f;

    public abstract bool ApplyEffect(GameObject target);
    public virtual void OnPickup(PlayerInventory inventory) { }
}