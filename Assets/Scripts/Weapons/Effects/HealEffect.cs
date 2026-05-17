using System;
using UnityEngine;

[Serializable]
public class HealEffect : IHitEffect
{
    [SerializeField] private int amount = 1;

    public void apply(GameObject target, GameObject attacker)
    {
        if (target.TryGetComponent(out Health health))
            health.takeHeal(amount);
    }
}