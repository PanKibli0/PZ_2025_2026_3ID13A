using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerUnit unit;
    [SerializeField] private float weaponSwitchCooldown = 0.3f;
    [SerializeField] private HotbarUI hotbar;

    private WeaponData currentWeapon;
    private float lastAttackTime;
    private float lastSwitchTime;

    public bool CanAttack { get; set; } = true;

    private void Awake()
    {
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found on Player!");
            return;
        }

        if (inventory.GetWeapon(0) != null)
            SwitchWeapon(0);
    }

    // DEBUG - OLD INPUT
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SwitchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SwitchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SwitchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SwitchWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SwitchWeapon(4);
    }
    // END DEBUG

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!CanAttack) return;
        if (!context.performed || currentWeapon == null) return;
        if (Time.time < lastAttackTime + currentWeapon.attack.cooldown) return;

        Vector2 aimDirection = GetAimDirection();
        Vector2 origin = (Vector2)transform.position + aimDirection * currentWeapon.attackOffset;

        HitContext hitContext = currentWeapon.attack.CreateContext(gameObject, unit.faction, origin, aimDirection);
        currentWeapon.attack.Execute(hitContext);
        lastAttackTime = Time.time;
    }

    private Vector2 GetAimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return (mousePos - (Vector2)transform.position).normalized;
    }

    public void SwitchWeapon(int index)
    {
        if (Time.time < lastSwitchTime + weaponSwitchCooldown)
            return;

        WeaponData weapon = inventory.GetWeapon(index);

        if (weapon == null)
            return;

        currentWeapon = weapon;
        lastSwitchTime = Time.time;
        Debug.Log("Switched weapon: " + weapon.weaponName);

        if (hotbar != null)
            hotbar.SetSelected(index);
    }

    public void SetHotbar(HotbarUI ui)
    {
        hotbar = ui;
    }
}
