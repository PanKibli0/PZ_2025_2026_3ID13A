using UnityEngine;

[System.Serializable]
public class FrozenHitEffect : IHitEffect
{
    [SerializeField]
    private float duration = 5f;

    [SerializeField]
    [Range(0.1f, 1f)]
    private float slowMultiplier = 0.5f;

    public void apply(GameObject target, HitContext context)
    {
        if (target == null)
            return;

        IStatusEffectReceiver receiver =
            target.transform.root.GetComponent<IStatusEffectReceiver>();

        if (receiver == null)
            return;

        PlayerMovement movement =
            target.transform.root.GetComponent<PlayerMovement>();

        if (movement == null)
            return;

        receiver.AddEffect(
            new FrozenStatusEffect(
                movement,
                duration,
                slowMultiplier
            )
        );
    }
}