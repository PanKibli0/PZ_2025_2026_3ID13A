using UnityEngine;

[System.Serializable]
public class FrozenHitEffect : IHitEffect
{
    [SerializeField] private float duration = 5f;
    [SerializeField][Range(0.1f, 1f)] private float slowMultiplier = 0.5f;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new FrozenStatusEffect(unit.moveHandler, duration, slowMultiplier));
    }
}