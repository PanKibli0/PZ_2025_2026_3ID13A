using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Health health = new Health();
    public Faction faction = new Faction();

    protected virtual void Awake()
    {
        health.OnDeath += Death;
    }

    protected abstract void Death();
}
