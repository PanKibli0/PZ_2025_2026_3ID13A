using UnityEngine;

[CreateAssetMenu(menuName = "Upgrades/Stat Upgrade")]
public class StatUpgrade : UpgradeData
{
    public PlayerStatType statType;
    public float value;

    public override void Apply(GameObject player)
    {

        PlayerStats stats = player.GetComponentInChildren<PlayerStats>();
        PlayerUnit unit = player.GetComponent<PlayerUnit>();

        switch (statType)
        {
            case PlayerStatType.Damage:
                stats.AddDamage(value);
                break;

            case PlayerStatType.AttackSpeed:
                stats.AddAttackSpeed(value);
                break;

            case PlayerStatType.MoveSpeed:
                stats.AddMoveSpeed(value);
                break;

            case PlayerStatType.CriticalChance:
                stats.AddCriticalChance(value);
                break;

            case PlayerStatType.CriticalDamage:
                stats.AddCriticalDamage(value);
                break;

            case PlayerStatType.DodgeChance:
                stats.AddDodgeChance(value);
                break;

            case PlayerStatType.HealthRegen:
                stats.AddHealthRegen(value);
                break;

            case PlayerStatType.Luck:
                stats.AddLuck(value);
                break;

            case PlayerStatType.MaxHealth:
                unit.health.SetMaxHealth(unit.health.maxHealth + Mathf.RoundToInt(value));
                break;
        }
    }
}