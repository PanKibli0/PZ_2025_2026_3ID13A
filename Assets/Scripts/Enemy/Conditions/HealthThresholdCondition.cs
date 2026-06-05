using UnityEngine;

[System.Serializable]
public class HealthThresholdCondition : ICondition
{
    [SerializeField] private float thresholdPercent = 0.5f; 

    public bool isMet(GameObject enemy, Transform player, Health health)
    {
        if (health == null) return false;

        float percent = (float)health.getCurrentHealth() / health.getMaxHealth();
        return percent <= thresholdPercent;
    }
}