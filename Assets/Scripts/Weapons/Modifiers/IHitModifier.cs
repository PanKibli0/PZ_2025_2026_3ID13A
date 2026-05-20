using UnityEngine;

public interface IHitModifier
{
    void apply(GameObject target, GameObject attacker);
}