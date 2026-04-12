using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireCooldown = 0.2f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float spawnOffset = 0.8f;

    private float cooldownTimer;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (cooldownTimer > 0)
            cooldownTimer -= Time.deltaTime;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed && cooldownTimer <= 0)
        {
            shoot();
            cooldownTimer = fireCooldown;
        }
    }

    private void shoot()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePosition.z = 0;
        Vector2 direction = (mousePosition - transform.position).normalized;

        Vector3 spawnPosition = transform.position + (Vector3)direction * spawnOffset;

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
        Projectile projScript = projectile.GetComponent<Projectile>();
        if (projScript != null)
            projScript.init(direction, damage, gameObject);
    }

    public void setDamage(int newDamage)
    {
        damage = newDamage;
    }
}