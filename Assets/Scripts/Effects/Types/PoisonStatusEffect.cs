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
        ApplyPoisonDamage();
        tickTimer = 1f;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        tickTimer -= deltaTime;

        if (tickTimer <= 0f)
        {
            ApplyPoisonDamage();
            tickTimer = 1f;
        }

        if (duration <= 0f)
            Finished = true;
    }

    private void ApplyPoisonDamage()
    {
        if (health == null)
        {
            Finished = true;
            return;
        }

        if (health.getCurrentHealth() <= 1)
            return;

        health.takeDamage(1);
    }

    public override void OnExpire()
    {
    }

    public override void Refresh()
    {
        duration = MaxDuration;
    }
}