using UnityEngine;

public class BurnStatusEffect : StatusEffect
{
    private readonly Health health;

    private float duration;
    private float tickInterval;
    private int damagePerTick;

    private float tickTimer;

    public BurnStatusEffect(
        Health health,
        float duration,
        float tickInterval,
        int damagePerTick)
    {
        this.health = health;
        this.duration = duration;
        this.tickInterval = tickInterval;
        this.damagePerTick = damagePerTick;
    }

    public override void OnApply()
    {
        tickTimer = tickInterval;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        tickTimer -= deltaTime;

        if (tickTimer <= 0f)
        {
            health.TakeDamage(damagePerTick);
            tickTimer = tickInterval;
        }

        if (duration <= 0f)
        {
            Finished = true;
        }
    }

    public override void OnExpire()
    {
    }
}