using System;

public enum FactionType
{
    Player,
    Enemy,
    Neutral
}

[Serializable]
public class Faction
{
    public FactionType factionType;

    public bool IsAlly(Faction other)
    {
        if (other == null) return false;
        return factionType == other.factionType;
    }

    public bool IsEnemy(Faction other)
    {
        if (other == null) return false;
        return factionType != other.factionType;
    }
}
