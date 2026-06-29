using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    [SerializeField] private PlayerUnit unit;
    [SerializeField] private PlayerData data;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerWeaponHandler weaponHandler;

    public void Init(HotbarUI hotbar, PlayerUI playerUI)
    {
        unit.faction.factionType = FactionType.Player;
        unit.health.Init(data.maxHealth);
        unit.moveHandler = playerMovement;
        unit.attackHandler = weaponHandler;

        playerUI.Init(unit.health);
        weaponHandler.SetHotbar(hotbar);
    }
}