using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitboxCollider;

    private HitContext hitContext;
    [SerializeField] private bool active;

    public void activate(HitContext context)
    {
        hitContext = context;
        active = true;
        hitboxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!active) return;
        if (other.TryGetComponent(out Hurtbox hurtbox))
            hurtbox.receiveHit(hitContext);
    }
}