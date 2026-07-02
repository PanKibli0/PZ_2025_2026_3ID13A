using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private PlayerStats playerStats;

    private float timer;

    private void Awake()
    {
        if (health == null)
            health = GetComponentInChildren<Health>();

        if (playerStats == null)
            playerStats = GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (playerStats.HealthRegen <= 0f)
            return;

        timer += Time.deltaTime;

        if (timer >= 1f)
        {
            timer = 0f;

            health.takeHeal(Mathf.RoundToInt(playerStats.HealthRegen));
        }
    }
}