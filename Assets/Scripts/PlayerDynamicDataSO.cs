using UnityEngine;

[CreateAssetMenu(fileName = "PlayerDynamicDataSO", menuName = "Scriptable Objects/PlayerDynamicDataSO")]
public class PlayerDynamicDataSO : ScriptableObject
{
    public int hp;
    public float linearVelocityDamping = 0.7f;
    public float movementSpeedMultiplier = 7;
    public float maxMovementSpeed = 5;
}
