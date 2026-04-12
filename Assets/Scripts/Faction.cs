using UnityEngine;

public class Faction : MonoBehaviour
{
    public FactionType factionType;
}

public enum FactionType
{
    None,
    Player,
    Enemy
}