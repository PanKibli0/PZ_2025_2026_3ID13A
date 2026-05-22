using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponHandler : MonoBehaviour
{
    [SerializeField] private WeaponData[] weaponSlots;
    [SerializeField] private Faction faction;


    private WeaponData currentWeapon;
    private float lastAttackTime;
    

    private void Awake()
    { 
        if (weaponSlots.Length > 0)
            switchWeapon(0);
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (!context.performed || currentWeapon == null) return;
        if (Time.time < lastAttackTime + currentWeapon.cooldown) return;

        Vector2 aimDirection = getAimDirection();

        Vector2 origin = (Vector2)transform.position + aimDirection * currentWeapon.attackOffset;

        HitContext hit = new HitContext(gameObject, faction, origin, aimDirection, currentWeapon);

        currentWeapon.attackPattern.execute(hit);
        lastAttackTime = Time.time;
    }

    private Vector2 getAimDirection()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return (mousePos - (Vector2)transform.position).normalized;
    }

    // Debug
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) switchWeapon(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) switchWeapon(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) switchWeapon(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) switchWeapon(3);
        if (Input.GetKeyDown(KeyCode.Alpha5)) switchWeapon(4);
    }
    // END Debug

    private void switchWeapon(int index)
    {
        if (index < weaponSlots.Length)
            currentWeapon = weaponSlots[index];
    }
}