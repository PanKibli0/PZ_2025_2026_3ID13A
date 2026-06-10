using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private Faction faction;
    [SerializeField] private float weaponSwitchCooldown = 0.3f;
    [SerializeField] private HotbarUI hotbar;


    private WeaponData currentWeapon;
    private float lastAttackTime;
    private float lastSwitchTime;

    private void Awake()
    {
        if (inventory == null)
        {
            Debug.LogError("PlayerInventory not found on Player!");
            return;
        }

        if (inventory.GetWeapon(0) != null)
            switchWeapon(0);
    }

    // DEBUG - OLD INPUT
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) switchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) switchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) switchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) switchWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) switchWeapon(4);
    }
    // END DEBUG

    public void onAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || currentWeapon == null) return;
        if (Time.time < lastAttackTime + currentWeapon.attack.cooldown) return;

        Vector2 aimDirection = getAimDirection();
        Vector2 origin = (Vector2)transform.position + aimDirection * currentWeapon.attackOffset;

        HitContext hitContext = currentWeapon.attack.createContext(gameObject, faction, origin, aimDirection);
        currentWeapon.attack.execute(hitContext);
        lastAttackTime = Time.time;
    }

    private Vector2 getAimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return (mousePos - (Vector2)transform.position).normalized;
    }

    public void switchWeapon(int index)
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