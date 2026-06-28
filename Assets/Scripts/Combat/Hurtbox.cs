using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private float lastHitTime = -999f;

    public Faction GetFaction()
    {
        return unit.faction;
    }

    public void ReceiveHit(HitContext context)
    {
        if (Time.time - lastHitTime < invulnerabilityTime) return;
        if (!context.attackerFaction.IsEnemy(unit.faction)) return;

        if (context.damage > 0)
            unit.health.TakeDamage(context.damage);

        foreach (var effect in context.effects)
            effect?.apply(gameObject, context);

        lastHitTime = Time.time;
    }
}
