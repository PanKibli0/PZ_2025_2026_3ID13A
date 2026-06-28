using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IKnockbackReceiver
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    [SerializeField] private float slipperyAcceleration = 9f;
    [SerializeField] private float slipperyDeceleration = 3f;

    private float speedMultiplier = 1f;
    private Vector2 moveDirection;
    private Vector2 slipperyVelocity;
    private float knockbackEndTime;
    private bool slipperyMovement;

    public bool CanMove { get; set; } = true;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
    }

    public void ApplyKnockback(Vector2 force, float duration = 0.15f)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(force, ForceMode2D.Impulse);
        knockbackEndTime = Time.time + duration;
    }

    private void FixedUpdate()
    {
        if (Time.time < knockbackEndTime)
            return;

        if (!CanMove)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        if (!slipperyMovement)
        {
            rb.linearVelocity = moveDirection * speed * speedMultiplier;
        }
        else
        {
            Vector2 targetVelocity = moveDirection * speed * speedMultiplier;

            if (moveDirection != Vector2.zero)
            {
                slipperyVelocity = Vector2.MoveTowards(
                    slipperyVelocity,
                    targetVelocity,
                    slipperyAcceleration * Time.fixedDeltaTime
                );
            }
            else
            {
                slipperyVelocity = Vector2.MoveTowards(
                    slipperyVelocity,
                    Vector2.zero,
                    slipperyDeceleration * Time.fixedDeltaTime
                );
            }

            rb.linearVelocity = slipperyVelocity;
        }
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        speedMultiplier = multiplier;
    }

    public float GetSpeedMultiplier()
    {
        return speedMultiplier;
    }

    public void SetSlipperyMovement(bool value)
    {
        slipperyMovement = value;

        if (!value)
            slipperyVelocity = Vector2.zero;
    }
}
