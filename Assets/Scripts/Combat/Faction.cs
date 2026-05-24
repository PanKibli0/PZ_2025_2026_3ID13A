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

    public bool isAlly(Faction other)
    {
        if (other == null) return false;
        return factionType == other.factionType;
    }

    public bool isEnemy(Faction other)
    {
        if (other == null) return false;        
        return factionType != other.factionType;
    }
}
