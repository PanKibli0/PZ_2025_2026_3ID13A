using UnityEngine;
using System.Collections;

[System.Serializable]
public class StabPattern : AttackPattern
{
    [SerializeField] private float forwardDistance = 2f;
    [SerializeField] private float activeTime = 0.15f;

    public override void Execute(HitContext context)
    {
        Hitbox hitbox = context.attacker.meleeHitbox;
        hitbox.StartCoroutine(StabRoutine(hitbox, context));
    }

    private IEnumerator StabRoutine(Hitbox hitbox, HitContext context)
    {
        Vector3 localDirection = hitbox.transform.parent.InverseTransformDirection(context.direction);
        hitbox.ActivateFor(context, activeTime);

        float elapsed = 0f;
        while (elapsed < activeTime)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / activeTime);
            float offset = progress * forwardDistance;

            hitbox.transform.localPosition = localDirection * offset;

            yield return null;
        }
    }
}