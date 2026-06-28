using UnityEngine;

public interface IKnockbackReceiver
{
    void ApplyKnockback(Vector2 force, float duration = 0.15f);
}
