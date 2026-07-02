using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private PlayerUnit unit;
    [SerializeField] private PlayerStats playerStats;

    private float timer;

    private void Awake()
    {
        if (unit == null)
            unit = GetComponentInParent<PlayerUnit>();

        if (playerStats == null)
            playerStats = GetComponentInChildren<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats.HealthRegen <= 0f)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;
            unit.health.TakeHeal(Mathf.RoundToInt(playerStats.HealthRegen));
        }
    }
}