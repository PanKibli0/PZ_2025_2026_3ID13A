using UnityEngine;

public class BubbleStatusEffect : StatusEffect
{
    private readonly PlayerMovement movement;
    private readonly PlayerWeaponHandler weaponHandler;

    private float timer;
    private readonly float duration;

    public BubbleStatusEffect(
        PlayerMovement movement,
        PlayerWeaponHandler weaponHandler,
        float duration)
    {
        this.movement = movement;
        this.weaponHandler = weaponHandler;
        this.duration = duration;
    }

    public override void OnApply()
    {
        movement.CanMove = false;
        weaponHandler.CanAttack = false;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= duration)
            Finished = true;
    }

    public override void OnExpire()
    {
        movement.CanMove = true;
        weaponHandler.CanAttack = true;
    }

    public override void Refresh()
    {
        timer = 0f;
    }
}