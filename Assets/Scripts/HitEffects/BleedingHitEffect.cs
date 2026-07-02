using UnityEngine;

[System.Serializable]
public class BleedingHitEffect : IHitEffect
{
    [SerializeField] private float duration = 15f;
    [SerializeField] private int damagePerTick = 5;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new BleedingStatusEffect(unit.health, duration, damagePerTick));
    }
}