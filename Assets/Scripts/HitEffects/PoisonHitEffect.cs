[System.Serializable]
public class PoisonHitEffect : IHitEffect
{
    public void Apply(Unit unit, HitContext context)
    {
        unit.statusEffects.AddEffect(new PoisonStatusEffect(unit.health));
    }
}