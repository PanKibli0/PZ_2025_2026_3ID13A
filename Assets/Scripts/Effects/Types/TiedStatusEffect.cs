using UnityEngine;

public class TiedStatusEffect : StatusEffect
{
    private readonly PlayerMovement movement;
    private float duration;
    private readonly float maxDuration;

    public TiedStatusEffect(PlayerMovement movement, float duration)
    {
        this.movement = movement;
        this.duration = duration;
        maxDuration = duration;
    }

    public override void OnApply()
    {
        movement.CanMove = false;
    }

    public override void Tick(float deltaTime)
    {
        duration -= deltaTime;

        if (duration <= 0f)
            Finished = true;
    }

    public override void OnExpire()
    {
        movement.CanMove = true;
    }

    public override void Refresh()
    {
        duration = maxDuration;
    }
}