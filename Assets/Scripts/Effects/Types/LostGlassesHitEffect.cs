using UnityEngine;

[System.Serializable]
public class LostGlassesHitEffect : IHitEffect
{
    [SerializeField]
    private float duration = 10f;

    public void apply(GameObject target, HitContext context)
    {
        if (target == null)
            return;

        PlayerStatusController statusController =
            target.transform.root.GetComponent<PlayerStatusController>();

        if (statusController == null)
            return;

        BlurController blurController =
            target.transform.root.GetComponent<BlurController>();

        if (blurController == null)
            return;

        statusController.AddEffect(
            new LostGlassesStatusEffect(
                blurController,
                duration
            )
        );
    }
}