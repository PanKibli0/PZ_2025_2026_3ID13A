using UnityEngine;

[System.Serializable]
public class HealHitEffect : IHitEffect
{
    [SerializeField] private int amount;

    public void apply(GameObject target, HitContext context)
    {
        if (target == null) return;

        if (!target.TryGetComponent(out Faction targetFaction)) return;
        if (!context.attacker.TryGetComponent(out Faction attackerFaction)) return;

        if (attackerFaction.isAlly(targetFaction))
        {
            if (target.TryGetComponent(out Health health))
                health.takeHeal(amount);
        }
    }
}