using UnityEngine;

public class SlipStatusEffect : StatusEffect
{
    private readonly PlayerMovement movement;

    private float duration;
    private readonly float maxDuration;

    public SlipStatusEffect(
        PlayerMovement movement,
        float duration)
    {
        this.movement = movement;
        this.duration = duration;
        maxDuration = duration;
    }

    public override void OnApply()
    {
        movement.SetSlipperyMovement(true);
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0f)
            Finished = true;
    }

    public override void OnExpire()
    {
        movement.SetSlipperyMovement(false);
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}