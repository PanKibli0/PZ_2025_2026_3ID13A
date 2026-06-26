using UnityEngine;

[System.Serializable]
public class BurnHitEffect : IHitEffect
{
    [SerializeField]
    private float duration = 5f;

    [SerializeField]
    private float tickInterval = 1f;

    [SerializeField]
    private int damagePerTick = 1;

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
            new BurnStatusEffect(
                health,
                duration,
                tickInterval,
                damagePerTick
            )
        );
    }
}