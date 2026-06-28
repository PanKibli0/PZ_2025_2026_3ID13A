using UnityEngine;

[System.Serializable]
public abstract class AttackPattern
{
    public abstract void Execute(HitContext context);
}
