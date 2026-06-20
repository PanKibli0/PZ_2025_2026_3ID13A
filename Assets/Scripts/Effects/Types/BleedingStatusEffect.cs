using UnityEngine;

public class BleedingStatusEffect : StatusEffect
{
    private readonly Health health;
    private readonly bool isPlayer;

    private float timer;
    private float tickTimer;

    private readonly float duration;

    public BleedingStatusEffect(
        Health health,
        bool isPlayer,
        float duration = 15f)
    {
        this.health = health;
        this.isPlayer = isPlayer;
        this.duration = duration;
    }

    public override void OnApply()
    {
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;
        tickTimer += deltaTime;

        if (tickTimer >= 1f)
        {
            tickTimer = 0f;

            if (isPlayer)
            {
                health.takeDamage(5);
            }
            else
            {
                int damage = Mathf.CeilToInt(
                    health.getCurrentHealth() * 0.05f
                );

                damage = Mathf.Max(1, damage);

                health.takeDamage(damage);
            }
        }

        if (timer >= duration)
            Finished = true;
    }

    public override void OnExpire()
    {
    }

    public override void Refresh()
    {
        timer = 0f;
        tickTimer = 0f;
    }
}