using UnityEngine;

[System.Serializable]
public class LostGlassesHitEffect : IHitEffect
{
    [SerializeField] private float duration = 10f;

    public void Apply(Unit unit, HitContext context)
    {
        if (unit.blurController == null) return;
        unit.statusEffects.AddEffect(new LostGlassesStatusEffect(unit.blurController, duration));
    }
}