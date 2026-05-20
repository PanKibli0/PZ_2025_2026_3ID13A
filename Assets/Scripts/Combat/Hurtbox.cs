using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Faction faction;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private float lastHitTime = -999f;

    public Faction getFaction() => faction;

    public void receiveHit(HitContext ctx)
    {
        if (Time.time - lastHitTime < invulnerabilityTime) return;
        if (ctx.attackerFaction.factionType == faction.factionType) return;

        if (health != null && ctx.damage > 0)
            health.takeDamage(ctx.damage);

        if (ctx.modifiers != null)
        {
            foreach (var m in ctx.modifiers)
                m.apply(gameObject, ctx.attacker);
        }

        lastHitTime = Time.time;
    }
}