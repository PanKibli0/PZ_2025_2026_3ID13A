using UnityEngine;
using System.Collections;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Collider2D hitboxCollider;

    private HitContext hitContext;
    private Coroutine activeRoutine;
    private Vector3 restLocalPosition;
    private Quaternion restLocalRotation;

    private void Awake()
    {
        restLocalPosition = transform.localPosition;
        restLocalRotation = transform.localRotation;
    }

    public void Activate(HitContext context)
    {
        hitContext = context;
        hitboxCollider.enabled = true;
    }

    public void ActivateFor(HitContext context, float duration)
    {
        if (activeRoutine != null)
            StopCoroutine(activeRoutine);

        Activate(context);
        activeRoutine = StartCoroutine(DeactivateAfter(duration));
    }

    private IEnumerator DeactivateAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
        hitboxCollider.enabled = false;
        transform.localPosition = restLocalPosition;
        transform.localRotation = restLocalRotation;
        activeRoutine = null;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Hurtbox hurtbox))
            hurtbox.ReceiveHit(hitContext);
    }
}