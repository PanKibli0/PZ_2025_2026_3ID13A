using UnityEngine;

[System.Serializable]
public abstract class AttackPattern
{
    public abstract void execute(HitContext context);
}