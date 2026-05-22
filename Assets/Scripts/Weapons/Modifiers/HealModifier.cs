using UnityEngine;

[System.Serializable]
public class HealModifier : IHitModifier
{
    [SerializeField] private int amount;

    public void apply(GameObject target, GameObject attacker)
    {
        if (!target.TryGetComponent(out Faction targetFaction)) return;
        if (!attacker.TryGetComponent(out Faction attackerFaction)) return;

        if (targetFaction.factionType != attackerFaction.factionType) return;

        if (target.TryGetComponent(out Health health))
            health.takeHeal(amount);
    }
}
