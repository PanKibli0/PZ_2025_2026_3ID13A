using UnityEngine;

[System.Serializable]
public class KnockbackHitEffect : IHitEffect
{
    [SerializeField] private float force;

    public void apply(GameObject target, HitContext context)
    {
        if (target == null) return;

        IKnockbackReceiver receiver = target.transform.root.GetComponent<IKnockbackReceiver>();
        if (receiver == null) return;

        receiver.ApplyKnockback(context.direction * force);
    }
}
