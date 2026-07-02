using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class PlayerWeaponHandler : MonoBehaviour, IAttackHandler
{
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerUnit unit;
    [SerializeField] private float weaponSwitchCooldown = 0.3f;
    [SerializeField] private HotbarUI hotbar;
    [SerializeField] private PlayerStats playerStats;

    private WeaponData currentWeapon;
    private int currentIndex;
    private float lastAttackTime;
    private float lastSwitchTime;

    public bool CanAttack { get; set; } = true;
    public WeaponData CurrentWeapon
    {
        get { return currentWeapon; }
    }
    public int CurrentIndex
    {
        get { return currentIndex; }
    }

    public event Action OnAttackStarted;
    public event Action OnAttackEnded;
    public event Action<int> OnWeaponSwitched;

    private void Awake()
    {
        if (inventory == null)
            Debug.LogError("PlayerInventory not found on Player!");
    }

    private void Start()
    {
        if (inventory != null && inventory.GetWeapon(0) != null)
            SwitchWeapon(0);
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (!CanAttack) return;
        if (!context.performed || currentWeapon == null) return;
        float cooldown = currentWeapon.attack.cooldown / playerStats.AttackSpeedMultiplier;

        if (Time.time < lastAttackTime + cooldown)
            return;

        Vector2 aimDirection = GetAimDirection();
        Vector2 origin = (Vector2)transform.position + aimDirection * currentWeapon.attackOffset;

        HitContext hitContext = currentWeapon.attack.CreateContext(unit, origin, aimDirection);

        hitContext.damage = Mathf.RoundToInt(hitContext.damage * playerStats.DamageMultiplier);

        if (UnityEngine.Random.value <= playerStats.CriticalChance / 100f)
        {
            hitContext.isCritical = true;
            hitContext.damage = Mathf.RoundToInt(hitContext.damage * playerStats.CriticalDamage);
        }

        OnAttackStarted?.Invoke();
        currentWeapon.attack.Execute(hitContext);
        OnAttackEnded?.Invoke();

        lastAttackTime = Time.time;
    }

    public void OnSwitchWeapon1(InputAction.CallbackContext context)
    {
        if (context.performed) SwitchWeapon(0);
    }

    public void OnSwitchWeapon2(InputAction.CallbackContext context)
    {
        if (context.performed) SwitchWeapon(1);
    }

    public void OnSwitchWeapon3(InputAction.CallbackContext context)
    {
        if (context.performed) SwitchWeapon(2);
    }

    public void OnSwitchWeapon4(InputAction.CallbackContext context)
    {
        if (context.performed) SwitchWeapon(3);
    }

    public void OnSwitchWeapon5(InputAction.CallbackContext context)
    {
        if (context.performed) SwitchWeapon(4);
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
        currentIndex = index;
        lastSwitchTime = Time.time;
        Debug.Log($"Switched weapon: {weapon.weaponName}");

        OnWeaponSwitched?.Invoke(index);

        if (hotbar != null)
            hotbar.SetSelected(index);
    }

    public void SetHotbar(HotbarUI ui)
    {
        hotbar = ui;
    }
}