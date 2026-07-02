using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public Health health = new Health();
    public Faction faction = new Faction();
    public IMoveHandler moveHandler;
    public IAttackHandler attackHandler;
    public StatusEffectController statusEffects;
    public BlurController blurController;
    public Hitbox meleeHitbox;

    protected virtual void Awake()
    {
        health.OnDeath += Death;
    }

    protected abstract void Death();
}