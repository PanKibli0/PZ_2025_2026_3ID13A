using UnityEngine;

[System.Serializable]
public class KnockbackHitEffect : IHitEffect
{
    [SerializeField] private float force;

    public void apply(GameObject target, HitContext context)
    {
        if (target == null) return;

        Rigidbody2D rb = target.GetComponentInParent<Rigidbody2D>();
        if (rb == null) return;

        rb.linearVelocity = context.direction * force;
    }
}