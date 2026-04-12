using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Objects/UnitDataSO")]
public class UnitDataSO : ScriptableObject
{
    public int maxHealth;
    public float moveSpeed;
    public float linearDamping;
}