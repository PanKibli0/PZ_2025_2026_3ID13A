using UnityEngine;

public abstract class UpgradeData : ScriptableObject
{
    public string upgradeName;

    [TextArea] public string description;

    public Sprite icon;

    public abstract void Apply(GameObject player);
}