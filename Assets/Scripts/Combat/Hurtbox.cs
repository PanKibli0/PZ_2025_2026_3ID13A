
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Faction faction;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private bool canBeHit = true;
    private float invulnerabilityTimer;

    public Faction getFaction()
    {
        return faction;
    }

    private void Update()
    {
        if (canBeHit) return;

        invulnerabilityTimer -= Time.deltaTime;
        if (invulnerabilityTimer <= 0f)
            canBeHit = true;
    }

    public void receiveHit(AttackContext context)
    {
        if (!canBeHit) return; // MOZE BYC PROBLEM PRZY SZYBKICH ATAKACH nawet jak invulnerabilityTime = 0
        if (context.attackerFaction.factionType == faction.factionType) return;

        foreach (var effect in context.effects)
            effect.apply(gameObject, context.attacker);

        canBeHit = false;
        invulnerabilityTimer = invulnerabilityTime;
    }
}