public class TiedStatusEffect : StatusEffect
{
    private readonly IMoveHandler moveHandler;
    private float duration;
    private readonly float maxDuration;

    public TiedStatusEffect(IMoveHandler moveHandler, float duration)
    {
        this.moveHandler = moveHandler;
        this.duration = duration;
        this.maxDuration = duration;
    }

    public override void OnApply()
    {
        moveHandler.CanMove = false;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;
        if (duration <= 0f) Finished = true;
    }

    public override void OnExpire()
    {
        moveHandler.CanMove = true;
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}