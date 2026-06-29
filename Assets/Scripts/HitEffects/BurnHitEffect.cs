using UnityEngine;

[System.Serializable]
public class BurnHitEffect : IHitEffect
{
    [SerializeField] private float duration = 5f;
    [SerializeField] private float tickInterval = 1f;
    [SerializeField] private int damagePerTick = 1;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new BurnStatusEffect(unit.health, duration, tickInterval, damagePerTick));
    }
}