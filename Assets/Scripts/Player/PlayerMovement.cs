using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IKnockbackReceiver
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed = 5f;

    private Vector2 moveDirection;
    private float knockbackEndTime;
    public bool CanMove { get; set; } = true;

    public void onMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>().normalized;
    }

    public void applyKnockback(Vector2 force, float duration = 0.15f)
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

        rb.linearVelocity = moveDirection * speed;
    }
}