using UnityEngine;
using System.Collections.Generic;

public class EnemyMovementHandler : MonoBehaviour, IMoveHandler
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private EnemyUnit unit;
    [SerializeField] private List<MovementPhase> phases;
    [SerializeField] private SlipperySettings slipperySettings;

    private IEnemyMove currentMove;
    private float currentSpeed;
    private Transform player;
    private float knockbackEndTime;
    private float speedMultiplier = 1f;
    private bool slipperyMovement;
    private Vector2 slipperyVelocity;

    public bool CanMove { get; set; } = true;

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = Mathf.Max(0f, multiplier);
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public void SetSlipperyMovement(bool value)
    {
        slipperyMovement = value;
        if (!value) slipperyVelocity = Vector2.zero;
    }

    public void ApplyKnockback(Vector2 force, float duration = 0.15f)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        knockbackEndTime = Time.time + duration;
    }

    public void Init(List<MovementPhase> movementPhases, Transform playerTransform)
    {
        phases = movementPhases;
        player = playerTransform;
    }

    private void Update()
    {
        if (player == null) return;

        MovementPhase activePhase = null;

        foreach (var phase in phases)
        {
            if (phase.condition == null || phase.condition.isMet(gameObject, player, unit.health))
            {
                activePhase = phase;
                break;
            }
        }

        if (activePhase == null)
        {
            if (currentMove != null)
            {
                currentMove = null;
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }

        if (currentMove != activePhase.movement)
        {
            currentMove = activePhase.movement;
            currentSpeed = activePhase.speed;
            currentMove?.init(player);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentMove?.onCollisionWithWall();
    }

    private void FixedUpdate()
    {
        if (Time.time < knockbackEndTime) return;

        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (currentMove == null) return;

        Vector2 direction = currentMove.getMovement(rb.position, Time.fixedDeltaTime, currentSpeed);
        float effectiveSpeed = currentSpeed * speedMultiplier;

        if (!slipperyMovement)
        {
            rb.linearVelocity = direction * effectiveSpeed;
        }
        else
        {
            Vector2 target = direction * effectiveSpeed;

            if (direction != Vector2.zero)
                slipperyVelocity = Vector2.MoveTowards(slipperyVelocity, target, slipperySettings.acceleration * Time.fixedDeltaTime);
            else
                slipperyVelocity = Vector2.MoveTowards(slipperyVelocity, Vector2.zero, slipperySettings.deceleration * Time.fixedDeltaTime);

            rb.linearVelocity = slipperyVelocity;
        }
    }
}