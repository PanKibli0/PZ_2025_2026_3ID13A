using UnityEngine;

[System.Serializable]
public class BubbleHitEffect : IHitEffect
{
    [SerializeField] private float duration = 3f;

    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new BubbleStatusEffect(unit.moveHandler, unit.attackHandler, duration));
    }
}