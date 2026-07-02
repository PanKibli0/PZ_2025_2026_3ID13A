using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerUnit unit;
    [SerializeField] private PlayerData data;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private PlayerStats playerStats;

    public void Init(HotbarUI hotbar, PlayerUI playerUI)
    {
        unit.faction.factionType = FactionType.Player;
        unit.health.Init(data.maxHealth);
        unit.health.SetPlayerStats(playerStats);
        unit.moveHandler = playerMovement;
        unit.attackHandler = weaponHandler;

        playerUI.Init(unit.health);
        weaponHandler.SetHotbar(hotbar);
    }
}