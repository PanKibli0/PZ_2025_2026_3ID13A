using UnityEngine;

public class EnemyRush : MonoBehaviour
{
    [SerializeField] private float rushSpeed = 12f;
    [SerializeField] private float rushDuration = 0.25f;
    [SerializeField] private float cooldownDuration = 2f;
    [SerializeField] private float maxRushDistance = 3f;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color rushColor = Color.red;
    [SerializeField] private GameObject rushHitBox;
    [SerializeField] private PlayerReferenceSO playerReference;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private EnemyMovement movement;

    private bool isRushing;
    private bool isOnCooldown;
    private float rushTimer;
    private float cooldownTimer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movement = GetComponent<EnemyMovement>();

        spriteRenderer.color = normalColor;
        if (rushHitBox != null) rushHitBox.SetActive(false);
    }

    private void FixedUpdate()
    {
        if (playerReference == null || playerReference.playerInstance == null) return;

        Transform player = playerReference.playerInstance.transform;

        if (isRushing)
        {
            rushTimer -= Time.fixedDeltaTime;
            if (rushTimer <= 0f)
            {
                isRushing = false;
                isOnCooldown = true;
                cooldownTimer = cooldownDuration;
                rb.linearVelocity = Vector2.zero;
                spriteRenderer.color = normalColor;
                if (rushHitBox != null) rushHitBox.SetActive(false);
                if (movement != null) movement.enabled = true;
            }
        }
        else if (isOnCooldown)
        {
            cooldownTimer -= Time.fixedDeltaTime;
            if (cooldownTimer <= 0f)
                isOnCooldown = false;

            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            float distToPlayer = Vector2.Distance(transform.position, player.position);
            if (distToPlayer < maxRushDistance)
                startRush(player);
        }
    }

    private void startRush(Transform player)
    {
        isRushing = true;
        rushTimer = rushDuration;

        Vector2 direction = (player.position - transform.position).normalized;

        if (movement != null) movement.enabled = false;

        rb.linearVelocity = direction * rushSpeed;
        spriteRenderer.color = rushColor;

        if (rushHitBox != null) rushHitBox.SetActive(true);
    }
}