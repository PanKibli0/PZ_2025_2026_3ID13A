using UnityEngine;
using System.Collections.Generic;

public class EnemyAttackHandler : MonoBehaviour
{
    [SerializeField] private List<AttackPhase> phases;
    [SerializeField] private float phaseChangeCooldown = 0.3f;
    [SerializeField] private EnemyUnit unit;

    private Transform player;
    private Dictionary<AttackData, float> lastAttackTimes;
    private AttackPhase currentPhase;
    private float lastPhaseChangeTime;

    public void Init(List<AttackPhase> attackPhases, Transform playerTransform)
    {
        phases = attackPhases;
        player = playerTransform;
        lastAttackTimes = new Dictionary<AttackData, float>();
    }

    private void Update()
    {
        if (player == null) return;

        AttackPhase newPhase = SelectPhase();

        if (newPhase != currentPhase && Time.time >= lastPhaseChangeTime + phaseChangeCooldown)
        {
            currentPhase = newPhase;
            lastPhaseChangeTime = Time.time;
        }

        if (currentPhase != null && CanAttack(currentPhase.attack))
            ExecuteAttack(currentPhase.attack);
    }

    private AttackPhase SelectPhase()
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

    private bool CanAttack(AttackData attack)
    {
        if (!lastAttackTimes.ContainsKey(attack)) return true;
        return Time.time >= lastAttackTimes[attack] + attack.cooldown;
    }

    private void ExecuteAttack(AttackData attack)
    {
        Vector2 direction = (player.position - unit.transform.position).normalized;
        Vector2 origin = unit.transform.position + (Vector3)direction * 0.5f;

        HitContext context = attack.CreateContext(gameObject, unit.faction, origin, direction);
        attack.Execute(context);

        lastAttackTimes[attack] = Time.time;
    }
}
