using UnityEngine;

public class FrozenStatusEffect : StatusEffect
{
    private readonly PlayerMovement movement;

    private float duration;
    private readonly float maxDuration;

    private readonly float slowMultiplier;

    public FrozenStatusEffect(
        PlayerMovement movement,
        float duration,
        float slowMultiplier)
    {
        this.movement = movement;
        this.duration = duration;
        this.maxDuration = duration;
        this.slowMultiplier = slowMultiplier;
    }

    public override void OnApply()
    {
        movement.SetSpeedMultiplier(slowMultiplier);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0f)
            Finished = true;
    }

    public override void OnExpire()
    {
        movement.SetSpeedMultiplier(1f);
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}