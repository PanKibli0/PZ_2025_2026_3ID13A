using UnityEngine;

[System.Serializable]
public class TiedHitEffect : IHitEffect
{
    [SerializeField] private float duration = 5f;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new TiedStatusEffect(unit.moveHandler, duration));
    }
}