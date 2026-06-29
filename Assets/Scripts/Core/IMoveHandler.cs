using UnityEngine;

public interface IMoveHandler
{
    bool CanMove { get; set; }
    void SetSpeedMultiplier(float multiplier);
    float GetSpeedMultiplier();
    void SetSlipperyMovement(bool value);
    void ApplyKnockback(Vector2 force, float duration);
}