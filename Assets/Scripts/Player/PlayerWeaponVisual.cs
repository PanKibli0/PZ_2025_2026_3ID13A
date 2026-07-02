using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeaponVisual : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private PlayerWeaponHandler weaponHandler;
    [SerializeField] private float rotationSpeedDegPerSec = 720f;

    private bool isAttacking;
    private WeaponData lastWeapon;

    private void OnEnable()
    {
        weaponHandler.OnAttackStarted += HandleAttackStarted;
        weaponHandler.OnAttackEnded += HandleAttackEnded;
    }

    private void OnDisable()
    {
        weaponHandler.OnAttackStarted -= HandleAttackStarted;
        weaponHandler.OnAttackEnded -= HandleAttackEnded;
    }

    private void HandleAttackStarted()
    {
        isAttacking = true;
    }

    private void HandleAttackEnded()
    {
        isAttacking = false;
    }

    private void Update()
    {
        if (weaponHandler.CurrentWeapon != lastWeapon)
            RefreshWeaponVisual();

        if (isAttacking) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 direction = (mousePos - (Vector2)transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeedDegPerSec * Time.deltaTime);
    }

    private void RefreshWeaponVisual()
    {
        lastWeapon = weaponHandler.CurrentWeapon;

        if (lastWeapon == null || lastWeapon.weaponSprite == null)
        {
            spriteRenderer.enabled = false;
            return;
        }

        spriteRenderer.enabled = true;
        spriteRenderer.sprite = lastWeapon.weaponSprite;

        Vector2 nativeSize = lastWeapon.weaponSprite.bounds.size;
        Vector2 scale = new Vector2(lastWeapon.targetSize.x / nativeSize.x, lastWeapon.targetSize.y / nativeSize.y);
        transform.localScale = new Vector3(scale.x, scale.y, 1f);
    }
}