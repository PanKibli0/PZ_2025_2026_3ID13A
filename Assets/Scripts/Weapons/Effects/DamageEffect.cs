using System;
using UnityEngine;

[Serializable]
public class DamageEffect : IHitEffect
{
    [SerializeField] private int damage = 1;

    public void apply(GameObject target, GameObject attacker)
    {
        if (target.TryGetComponent(out Health health))
            health.takeDamage(damage);
    }
}