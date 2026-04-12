using UnityEngine;

public class Healbox : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    [SerializeField] private GameObject owner;

    private FactionType ownerFaction;
    private bool factionCached;

    private void Awake()
    {
        cacheFaction();
    }

    public void init(int healAmount, GameObject owner)
    {
        this.healAmount = healAmount;
        this.owner = owner;
        cacheFaction();
    }

    private void cacheFaction()
    {
        if (owner == null) return;

        Faction f = owner.GetComponent<Faction>();
        if (f != null)
        {
            ownerFaction = f.factionType;
            factionCached = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!factionCached) return;

        if (other.TryGetComponent<Hurtbox>(out var hurtbox))
        {
            Faction targetFaction = hurtbox.GetComponentInParent<Faction>();

            if (ownerFaction != FactionType.None && targetFaction != null && targetFaction.factionType != ownerFaction)
                return;

            hurtbox.heal(healAmount, owner);
        }
    }
}