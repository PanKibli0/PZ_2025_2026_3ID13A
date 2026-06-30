using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask hurtboxLayer;
    [SerializeField] private LayerMask destroyOnLayers;

    private HitContext hitContext;

    public void Setup(float speed, Vector2 direction, float lifetime, HitContext context, Sprite sprite, Vector2 size)
    {
        rb.linearVelocity = direction.normalized * speed;
        hitContext = context;

        if (sprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;

            Vector2 nativeSize = sprite.bounds.size;
            Vector2 scale = new Vector2(size.x / nativeSize.x, size.y / nativeSize.y);
            transform.localScale = new Vector3(scale.x * 0.2f, scale.y * 0.2f, 1f);
        }

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
        if (hurtbox.GetFaction().factionType == hitContext.attacker.faction.factionType) return;

        hurtbox.ReceiveHit(hitContext);
        Destroy(gameObject);
    }
}