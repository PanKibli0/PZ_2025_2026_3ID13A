using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitboxCollider;

    private AttackContext attackContext;
    [SerializeField] private bool active;

    public void activate(AttackContext context)
    {
        attackContext = context;
        active = true;
        hitboxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active) return;

        if (other.TryGetComponent(out Hurtbox hurtbox))
            hurtbox.receiveHit(attackContext);
    }
}
