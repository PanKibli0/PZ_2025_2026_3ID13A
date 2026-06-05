using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponData[] weaponSlots;
    [SerializeField] private Faction faction;
    [SerializeField] private float weaponSwitchCooldown = 0.3f;

    private WeaponData currentWeapon;
    private float lastAttackTime;
    private float lastSwitchTime;

    private void Awake()
    {
        if (weaponSlots.Length > 0)
            switchWeapon(0);
    }

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

    private void switchWeapon(int index)
    {
        if (Time.time < lastSwitchTime + weaponSwitchCooldown) return;
        if (index < weaponSlots.Length && weaponSlots[index] != null)
        {
            currentWeapon = weaponSlots[index];
            lastSwitchTime = Time.time;
        }
    }
}