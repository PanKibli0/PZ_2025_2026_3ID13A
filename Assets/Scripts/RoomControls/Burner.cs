using UnityEngine;
using System.Collections;

public class Burner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Hitbox hitbox;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Timing")]
    [SerializeField] private float initialDelay = 3f;
    [SerializeField] private float minInterval = 2f;
    [SerializeField] private float maxInterval = 5f;
    [SerializeField] private float warningTime = 0.6f;
    [SerializeField] private float activeTime = 0.8f;

    [Header("Damage")]
    [SerializeField] private int minDamage = 1;
    [SerializeField] private int maxDamage = 5;

    [Header("Visual")]
    [SerializeField] private float warningAlpha = 0.5f;

    private BurnerUnit burnerUnit;

    private void Awake()
    {
        burnerUnit = gameObject.AddComponent<BurnerUnit>();
        burnerUnit.faction.factionType = FactionType.Neutral;

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

            ActivateBurner();
            yield return new WaitForSeconds(activeTime);

            SetAlpha(0f);

            float waitTime = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(waitTime);
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

    private void ActivateBurner()
    {
        Vector2 origin = transform.position;
        int damage = Random.Range(minDamage, maxDamage + 1);
        HitContext context = new HitContext(burnerUnit, origin, Vector2.zero, damage, null);

        hitbox.ActivateFor(context, activeTime);
        SetAlpha(1f);
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