using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AttackData
{
    [Header("Attack Info")]
    public int damage;
    public float cooldown;

    [Header("Attack Effects")]
    [SerializeReference]
    public List<IHitEffect> effects;

    [Header("Attack Pattern")]
    [SerializeReference]
    public AttackPattern pattern;


    public void execute(HitContext context)
    {
        pattern.execute(context);
    }
}