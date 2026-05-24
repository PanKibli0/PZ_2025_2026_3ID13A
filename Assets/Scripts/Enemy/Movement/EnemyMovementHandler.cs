using UnityEngine;
using System.Collections.Generic;

public class EnemyMovementHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Health health;
    [SerializeField] private List<MovementPhase> phases;
    

    private IEnemyMove currentMove;
    private float currentSpeed;
    private Transform player;
    

    public void init(List<MovementPhase> movementPhases, Transform playerTransform)
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
            bool met = phase.condition == null || phase.condition.isMet(gameObject, player, health);

            if (met)
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
        if (currentMove == null) return;

        Vector2 direction = currentMove.getMovement(rb.position, Time.fixedDeltaTime, currentSpeed);
        rb.linearVelocity = direction * currentSpeed;
    }
}