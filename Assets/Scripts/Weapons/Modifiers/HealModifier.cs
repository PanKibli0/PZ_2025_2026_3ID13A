using UnityEngine;

[System.Serializable]
public class HealModifier : IHitModifier
{
    [SerializeField] private int amount;

    public void apply(GameObject target, GameObject attacker)
    {
        if (target.TryGetComponent(out Health health))
            health.takeHeal(amount);
    }
}
