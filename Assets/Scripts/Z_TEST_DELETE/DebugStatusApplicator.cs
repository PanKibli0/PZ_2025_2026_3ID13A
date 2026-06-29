using UnityEngine;

public class DebugStatusApplicator : MonoBehaviour
{
    [SerializeField] private Unit unit;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            unit.statusEffects.AddEffect(new PoisonStatusEffect(unit.health));

        if (Input.GetKeyDown(KeyCode.B))
            unit.statusEffects.AddEffect(new BurnStatusEffect(unit.health, 5f, 1f, 2));

        if (Input.GetKeyDown(KeyCode.T))
            unit.statusEffects.AddEffect(new TiedStatusEffect(unit.moveHandler, 3f));

        if (Input.GetKeyDown(KeyCode.F))
            unit.statusEffects.AddEffect(new FrozenStatusEffect(unit.moveHandler, 5f, 0.5f));

        if (Input.GetKeyDown(KeyCode.L))
            unit.statusEffects.AddEffect(new SlipStatusEffect(unit.moveHandler, 10f));

        if (Input.GetKeyDown(KeyCode.G))
            unit.statusEffects.AddEffect(new LostGlassesStatusEffect(unit.blurController, 10f));

        if (Input.GetKeyDown(KeyCode.O))
            unit.statusEffects.AddEffect(new BubbleStatusEffect(unit.moveHandler, unit.attackHandler, 3f));

        if (Input.GetKeyDown(KeyCode.K))
            unit.statusEffects.AddEffect(new BleedingStatusEffect(unit.health, 15f, 5));
    }
}