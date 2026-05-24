using UnityEngine;
using System.Collections.Generic;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private List<AttackPhase> phases;
    [SerializeField] private float phaseChangeCooldown = 0.3f; // REWORK ?? : SHared DATA SO WITH WEAPON SWITCH???
    [SerializeField] private Faction faction;

    private Transform player;
    
    private Dictionary<AttackData, float> lastAttackTimes;
    private AttackPhase currentPhase;
    private float lastPhaseChangeTime;

    public void init(List<AttackPhase> attackPhases, Transform playerTransform)
    {
        phases = attackPhases;
        player = playerTransform;
        lastAttackTimes = new Dictionary<AttackData, float>();
    }

    private void Update()
    {
        if (player == null) return;

        AttackPhase newPhase = selectPhase();

        if (newPhase != currentPhase && Time.time >= lastPhaseChangeTime + phaseChangeCooldown)
        {
            currentPhase = newPhase;
            lastPhaseChangeTime = Time.time;
        }

        if (currentPhase != null && canAttack(currentPhase.attack))
        {
            executeAttack(currentPhase.attack);
        }
    }

    private AttackPhase selectPhase()
    {
        List<AttackPhase> validPhases = new List<AttackPhase>();
        float totalWeight = 0f;

        foreach (var phase in phases)
        {
            if (phase.condition == null || phase.condition.isMet(gameObject, player, null))
            {
                validPhases.Add(phase);
                totalWeight += phase.weight;
            }
        }

        if (validPhases.Count == 0) return null;

        float random = Random.Range(0f, totalWeight);
        float current = 0f;

        foreach (var phase in validPhases)
        {
            current += phase.weight;
            if (random <= current) return phase;
        }

        return validPhases[0];
    }

    private bool canAttack(AttackData attack)
    {
        if (!lastAttackTimes.ContainsKey(attack)) return true;
        return Time.time >= lastAttackTimes[attack] + attack.cooldown;
    }

    private void executeAttack(AttackData attack)
    {
        Vector2 direction = (player.position - transform.parent.position).normalized;
        Vector2 origin = transform.parent.position + (Vector3)direction * 0.5f;

        HitContext context = attack.createContext(gameObject, faction, origin, direction);
        attack.execute(context);

        lastAttackTimes[attack] = Time.time;
    }
}