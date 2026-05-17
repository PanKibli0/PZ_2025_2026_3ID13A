using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Faction ownerFaction;

    public void setup(float speed, Vector2 direction, float lifetime, Faction owner)
    {
        rb.linearVelocity = direction.normalized * speed;
        ownerFaction = owner;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
            if (hurtbox.getFaction().factionType == ownerFaction.factionType)
                return;

        Destroy(gameObject);
    }
}
