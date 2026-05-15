using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private Faction ownerFaction;
    [SerializeField] private Collider2D hitboxCollider;
    [SerializeField] private float activeTime = 0.2f;
    [SerializeField] private HitData testHitData;
    // DEBUG

    private bool isActive;
    private float timer;
    private HitData currentHitData;

    // Debug
    private void Start()
    {
        activate();
    }

    public void activate()
    {
        activate(testHitData);
    }

    public void activate(HitData hitData)
    {
        currentHitData = hitData;
        isActive = true;
        timer = activeTime;
        hitboxCollider.enabled = true;
    }

    private void Update()
    {
        if (!isActive) return;

        timer -= Time.deltaTime;
        if (timer <= 0f)
            deactivate();
    }

    private void deactivate()
    {
        isActive = false;
        hitboxCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.TryGetComponent<Hurtbox>(out Hurtbox hurtbox))
            if (hurtbox.getFaction().factionType != ownerFaction.factionType)
                hurtbox.receiveHit(currentHitData);
    }
}
