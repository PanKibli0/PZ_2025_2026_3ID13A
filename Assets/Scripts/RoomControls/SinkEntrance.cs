using UnityEngine;

public class SinkEntrance : MonoBehaviour
{
    [SerializeField] private Collider2D entranceCollider;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out Hurtbox hurtbox)) return;
        if (hurtbox.GetFaction().factionType == FactionType.Player) return;

        Rigidbody2D rb = other.attachedRigidbody;
        if (rb == null) return;

        Vector2 closest = entranceCollider.ClosestPoint(rb.position);
        Vector2 pushDir = (rb.position - closest).normalized;

        if (pushDir == Vector2.zero)
            pushDir = ((Vector2)other.transform.position - (Vector2)entranceCollider.bounds.center).normalized;

        rb.position = closest + pushDir * 0.05f;
    }
}