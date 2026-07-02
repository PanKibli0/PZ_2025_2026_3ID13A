using UnityEngine;

[System.Serializable]
public class SlipHitEffect : IHitEffect
{
    [SerializeField] private float duration = 5f;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new SlipStatusEffect(unit.moveHandler, duration));
    }
}