using UnityEngine;

[CreateAssetMenu(fileName = "NewConsumableItem", menuName = "Items/Consumable Item")]
public class ConsumableItemData : ItemData
{
    [Header("Heal")]
    public int healAmount;

    public override bool ApplyEffect(GameObject player)
    {
        PlayerUnit unit = player.GetComponent<PlayerUnit>();

        if (unit == null)
            return false;

        if (healAmount > 0)
            unit.health.TakeHeal(healAmount);

        if (statusEffect != StatusEffectType.None)
        {
            StatusEffect effect = StatusEffectHelper.Create(
                statusEffect,
                unit,
                effectDuration,
                effectDamage,
                effectMultiplier);

            if (effect != null)
                unit.statusEffects.AddEffect(effect);
        }

        return true;
    }

    
}