public class BleedingStatusEffect : StatusEffect
{
    private readonly Health health;
    private float timer;
    private float tickTimer;
    private readonly float duration;
    private readonly int damagePerTick;

    public BleedingStatusEffect(Health health, float duration = 15f, int damagePerTick = 5)
    {
        this.health = health;
        this.duration = duration;
        this.damagePerTick = damagePerTick;
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
            health.TakeDamage(damagePerTick);
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