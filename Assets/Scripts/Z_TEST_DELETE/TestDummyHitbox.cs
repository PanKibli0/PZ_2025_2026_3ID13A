using UnityEngine;

public class TestDummyHitbox : MonoBehaviour
{
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private int damage = 5;
    [SerializeField] private Faction ownerFaction;

    private void Start()
    {
        HitContext ctx = new HitContext(gameObject, ownerFaction, Vector2.zero, Vector2.zero, damage);
        hitbox.activate(ctx);
    }
}