public static class StatusEffectHelper
{
    public static StatusEffect Create(
        StatusEffectType type,
        Unit unit,
        float duration,
        int damage,
        float multiplier)
    {
        switch (type)
        {
            case StatusEffectType.Burn:
                return new BurnStatusEffect(
                    unit.health,
                    duration,
                    1f,
                    damage);

            case StatusEffectType.Poison:
                return new PoisonStatusEffect(
                    unit.health,
                    duration);

            case StatusEffectType.Bleeding:
                return new BleedingStatusEffect(
                    unit.health,
                    duration,
                    damage);

            case StatusEffectType.Frozen:
                return new FrozenStatusEffect(
                    unit.moveHandler,
                    duration,
                    multiplier);

            case StatusEffectType.Slip:
                return new SlipStatusEffect(
                    unit.moveHandler,
                    duration);

            case StatusEffectType.Tied:
                return new TiedStatusEffect(
                    unit.moveHandler,
                    duration);

            case StatusEffectType.Bubble:
                return new BubbleStatusEffect(
                    unit.moveHandler,
                    unit.attackHandler,
                    duration);

            case StatusEffectType.LostGlasses:
                if (unit.blurController == null)
                    return null;

                return new LostGlassesStatusEffect(
                    unit.blurController,
                    duration);

            default:
                return null;
        }
    }
}