using UnityEngine;
using System.Collections;

[System.Serializable]
public class SwingPattern : AttackPattern
{
    [SerializeField] private float swingRadius = 1.5f;
    [SerializeField] private float activeTime = 0.25f;
    [SerializeField][Range(0f, 360f)] private float swingAngle = 90f;

    public override void Execute(HitContext context)
    {
        Hitbox hitbox = context.attacker.meleeHitbox;
        hitbox.StartCoroutine(SwingRoutine(hitbox, context));
    }

    private IEnumerator SwingRoutine(Hitbox hitbox, HitContext context)
    {
        float baseAngle = Mathf.Atan2(context.direction.y, context.direction.x) * Mathf.Rad2Deg - 90f;
        hitbox.ActivateFor(context, activeTime);

        float elapsed = 0f;
        while (elapsed < activeTime)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / activeTime);
            float angleOffset = (progress - 0.5f) * swingAngle;
            float currentAngle = baseAngle + angleOffset;

            Vector2 dir = Quaternion.Euler(0, 0, currentAngle) * Vector2.up;
            hitbox.transform.position = context.origin + dir * swingRadius;
            hitbox.transform.rotation = Quaternion.Euler(0f, 0f, currentAngle);

            yield return null;
        }
    }
}