using UnityEngine;

[CreateAssetMenu(menuName = "Movement/Slippery Settings")]
public class SlipperySettings : ScriptableObject
{
    public float acceleration = 9f;
    public float deceleration = 3f;
}