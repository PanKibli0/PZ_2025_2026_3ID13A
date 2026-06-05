using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private LayerMask hurtboxLayer;
    [SerializeField] private LayerMask destroyOnLayers;

    private HitContext hitContext;

    public void setup(float speed, Vector2 direction, float lifetime, HitContext context)
    {
        rb.linearVelocity = direction.normalized * speed;
        hitContext = context;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;

        if ((destroyOnLayers & (1 << layer)) != 0)
        {
            Destroy(gameObject);
            return;
        }

        if ((hurtboxLayer & (1 << layer)) == 0) return;
        if (!other.TryGetComponent(out Hurtbox hurtbox)) return;
        if (hurtbox.getFaction().factionType == hitContext.attackerFaction.factionType) return;

        hurtbox.receiveHit(hitContext);
        Destroy(gameObject);
    }
}