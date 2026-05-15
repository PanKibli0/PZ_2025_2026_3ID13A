using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Faction faction;
    [SerializeField] private float invulnerabilityTime = 0.5f;

    private bool canBeHit = true;
    private float invulnerabilityTimer;

    public Faction getFaction()
    {
        return faction;
    }

    public void receiveHit(HitData hitData)
    {
        if (!canBeHit) return;

        health.applyHit(hitData);
        canBeHit = false;
        invulnerabilityTimer = invulnerabilityTime;
    }

    private void Update()
    {
        if (canBeHit) return;

        invulnerabilityTimer -= Time.deltaTime;
        if (invulnerabilityTimer <= 0f)
            canBeHit = true;
    }
}
