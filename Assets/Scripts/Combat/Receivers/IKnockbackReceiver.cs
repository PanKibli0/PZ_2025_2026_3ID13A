using UnityEngine;

public interface IKnockbackReceiver
{
    void applyKnockback(Vector2 force, float duration = 0.15f);
}