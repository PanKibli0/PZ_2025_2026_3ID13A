using UnityEngine;
using System.Collections;

public class Burner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Collider2D hitboxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Timing")]
    [SerializeField] private float initialDelay = 3f;
    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 5f;
    [SerializeField] private float warningTime = 1.2f;
    [SerializeField] private float minActiveTime = 2f;
    [SerializeField] private float maxActiveTime = 3f;

    [Header("Damage")]
    [SerializeField] private int minDamage = 1;
    [SerializeField] private int maxDamage = 5;

    [Header("Visual")]
    [SerializeField] private float warningAlpha = 0.5f;

    private BurnerUnit burnerUnit;
    private float fixedActiveTime;

    private void Awake()
    {
        burnerUnit = gameObject.AddComponent<BurnerUnit>();
        burnerUnit.faction.factionType = FactionType.Neutral;

        fixedActiveTime = Random.Range(minActiveTime, maxActiveTime);

        hitboxCollider.enabled = false;
        SetAlpha(0f);
    }

    private void Start()
    {
        StartCoroutine(BurnerLoop());
    }

    private IEnumerator BurnerLoop()
    {
        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            yield return StartCoroutine(WarningPhase());

            SetAlpha(1f);
            hitboxCollider.enabled = true;

            yield return new WaitForSeconds(fixedActiveTime);

            hitboxCollider.enabled = false;
            SetAlpha(0f);

            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);
        }
    }

    private IEnumerator WarningPhase()
    {
        float elapsed = 0f;

        while (elapsed < warningTime)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, warningAlpha, elapsed / warningTime);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(warningAlpha);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryHit(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TryHit(other);
    }

    private void TryHit(Collider2D other)
    {
        if (!other.TryGetComponent(out Hurtbox hurtbox))
            return;

        Vector2 origin = transform.position;
        int damage = Random.Range(minDamage, maxDamage + 1);
        HitContext context = new HitContext(burnerUnit, origin, Vector2.zero, damage, null);

        hurtbox.ReceiveHit(context);
    }

    private void SetAlpha(float alpha)
    {
        Color c = spriteRenderer.color;
        c.a = alpha;
        spriteRenderer.color = c;
    }
}

public class BurnerUnit : Unit
{
    protected override void Death()
    {
    }
}