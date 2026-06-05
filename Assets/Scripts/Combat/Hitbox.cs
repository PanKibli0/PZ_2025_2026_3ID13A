using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitboxCollider;

    private HitContext hitContext;

    public void activate(HitContext context)
    {
        hitContext = context;
        hitboxCollider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
            hurtbox.receiveHit(hitContext);
    }
}