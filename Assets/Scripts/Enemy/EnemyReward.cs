using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    private EnemyData enemyData;
    [SerializeField] private GameObject moneyPrefab;

    public void Init(EnemyData data)
    {
        enemyData = data;
    }

    public void GiveRewards()
    {
        PlayerExperience player =
            FindFirstObjectByType<PlayerExperience>();

        if (player != null)
            player.AddExperience(enemyData.experienceReward);

        PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();
        float dropChance = enemyData.moneyDropChance;
        if (playerStats != null)
        {
            dropChance *= playerStats.Luck;
        }

        if (UnityEngine.Random.value <= dropChance)
        {
            GameObject money = Instantiate(
                moneyPrefab,
                transform.position,
                Quaternion.identity);

            MoneyPickup pickup = money.GetComponent<MoneyPickup>();

            if (pickup != null)
                pickup.Init(enemyData.moneyDropAmount);
        }
    }
}