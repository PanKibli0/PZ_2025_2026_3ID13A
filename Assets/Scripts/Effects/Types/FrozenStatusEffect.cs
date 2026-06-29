public class FrozenStatusEffect : StatusEffect
{
    private readonly IMoveHandler moveHandler;
    private float duration;
    private readonly float maxDuration;
    private readonly float slowMultiplier;

    public FrozenStatusEffect(IMoveHandler moveHandler, float duration, float slowMultiplier)
    {
        this.moveHandler = moveHandler;
        this.duration = duration;
        this.maxDuration = duration;
        this.slowMultiplier = slowMultiplier;
    }

    public override void OnApply()
    {
        moveHandler.SetSpeedMultiplier(slowMultiplier);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        if (duration <= 0f) Finished = true;
    }

    public override void OnExpire()
    {
        moveHandler.SetSpeedMultiplier(1f);
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}