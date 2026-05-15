using UnityEngine;

public enum FactionType
{
    Player,
    Enemy,
    Neutral
}

public class Faction : MonoBehaviour
{
    public FactionType factionType;
}
