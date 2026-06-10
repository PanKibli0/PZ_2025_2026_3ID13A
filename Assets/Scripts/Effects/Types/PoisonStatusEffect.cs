using UnityEngine;

public class PoisonStatusEffect : StatusEffect
{
    private readonly Health health;

    private float duration;
    private float tickTimer;
    private const float MaxDuration = 15f;

    public PoisonStatusEffect(Health health, float duration = 15f)
    {
        this.health = health;
        this.duration = duration;
    }
    
    public override void OnApply()
    {
        health.takeDamage(1);
        tickTimer = 1f;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        tickTimer -= deltaTime;

        if (tickTimer <= 0f)
        {
            health.takeDamage(1);
            tickTimer = 1f;
        }

        if (duration <= 0f)
        {
            Finished = true;
        }
    }

    public override void OnExpire()
    {
    }

        public override void Refresh()
    {
        duration = MaxDuration;
    }
}