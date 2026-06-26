public class LostGlassesStatusEffect : StatusEffect
{
    private readonly BlurController blurController;

    private float duration;
    private readonly float maxDuration;

    public LostGlassesStatusEffect(
        BlurController blurController,
        float duration)
    {
        this.blurController = blurController;
        this.duration = duration;
        this.maxDuration = duration;
    }

    public override void OnApply()
    {
        blurController.EnableBlur();
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0f)
            Finished = true;
    }

    public override void OnExpire()
    {
        blurController.DisableBlur();
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}