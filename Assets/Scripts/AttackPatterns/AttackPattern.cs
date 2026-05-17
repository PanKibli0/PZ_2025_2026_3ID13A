using UnityEngine;

[System.Serializable]
public abstract class AttackPattern
{
    public abstract void execute(AttackContext context);
}
