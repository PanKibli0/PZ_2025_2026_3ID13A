using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    private Vector2 direction;
    private int damage;
    private GameObject owner;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    public void init(Vector2 direction, int damage, GameObject owner)
    {
        this.direction = direction;
        this.damage = damage;
        this.owner = owner;

        Hitbox hitbox = GetComponent<Hitbox>();
        if (hitbox != null)
            hitbox.init(damage, owner);

        Faction ownerFaction = owner.GetComponent<Faction>();
        Faction projectileFaction = GetComponent<Faction>();
        if (ownerFaction != null && projectileFaction != null)
            projectileFaction.factionType = ownerFaction.factionType;

        rb.linearVelocity = direction * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}