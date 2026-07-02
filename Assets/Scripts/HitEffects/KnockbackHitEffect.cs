using UnityEngine;

[System.Serializable]
public class KnockbackHitEffect : IHitEffect
{
    [SerializeField] private float force;
    [SerializeField] private float duration = 0.15f;

    public void Apply(Unit unit, HitContext context)
    {
        unit.moveHandler.ApplyKnockback(context.direction * force, duration);
    }
}