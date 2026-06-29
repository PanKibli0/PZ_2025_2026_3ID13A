public class SlipStatusEffect : StatusEffect
{
    private readonly IMoveHandler moveHandler;
    private float duration;
    private readonly float maxDuration;

    public SlipStatusEffect(IMoveHandler moveHandler, float duration)
    {
        this.moveHandler = moveHandler;
        this.duration = duration;
        this.maxDuration = duration;
    }

    public override void OnApply()
    {
        moveHandler.SetSlipperyMovement(true);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        if (duration <= 0f) Finished = true;
    }

    public override void OnExpire()
    {
        moveHandler.SetSlipperyMovement(false);
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}