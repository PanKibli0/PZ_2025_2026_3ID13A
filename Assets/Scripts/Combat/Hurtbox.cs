using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Faction faction;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private float lastHitTime = -999f;

    public Faction getFaction()
    {
        return faction;
    }

    public void receiveHit(HitContext context)
    {
        if (Time.time - lastHitTime < invulnerabilityTime) return;

        if (context.attackerFaction != null && context.attackerFaction.isEnemy(faction))
        {
            if (health != null && context.damage > 0)
                health.takeDamage(context.damage);

            foreach (var effect in context.effects)
                if (effect != null)
                    effect.apply(gameObject, context);
        }

        lastHitTime = Time.time;
    }
}