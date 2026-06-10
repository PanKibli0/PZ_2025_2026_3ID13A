using UnityEngine;

[System.Serializable]
public class PoisonHitEffect : IHitEffect
{
    public void apply(GameObject target, HitContext context)
    {
        if (target == null)
            return;

        IStatusEffectReceiver receiver =
            target.transform.root.GetComponent<IStatusEffectReceiver>();

        if (receiver == null)
            return;

        Health health =
            target.transform.root.GetComponentInChildren<Health>();

        if (health == null)
            return;

        receiver.AddEffect(
            new PoisonStatusEffect(health)
        );
    }
}