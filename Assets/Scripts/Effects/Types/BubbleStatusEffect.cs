using UnityEngine;

public class BubbleStatusEffect : StatusEffect
{
    private readonly IMoveHandler moveHandler;
    private readonly IAttackHandler attackHandler;

    private float timer;
    private readonly float duration;

    public BubbleStatusEffect(IMoveHandler moveHandler, IAttackHandler attackHandler, float duration)
    {
        this.moveHandler = moveHandler;
        this.attackHandler = attackHandler;
        this.duration = duration;
    }

    public override void OnApply()
    {
        if (moveHandler != null) moveHandler.CanMove = false;
        if (attackHandler != null) attackHandler.CanAttack = false;
    }

    public override void Tick(float deltaTime)
    {
        timer += deltaTime;

        if (timer >= duration)
            Finished = true;
    }

    public override void OnExpire()
    {
        if (moveHandler != null) moveHandler.CanMove = true;
        if (attackHandler != null) attackHandler.CanAttack = true;
    }

    public override void Refresh()
    {
        timer = 0f;
    }
}