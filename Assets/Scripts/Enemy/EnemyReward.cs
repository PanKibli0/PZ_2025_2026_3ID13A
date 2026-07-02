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
        Debug.Log("GiveRewards");
        PlayerExperience player =
            FindFirstObjectByType<PlayerExperience>();

        if (player != null)
            player.AddExperience(enemyData.experienceReward);
        Debug.Log($"Drop chance: {enemyData.moneyDropChance}");

        PlayerStats playerStats = FindFirstObjectByType<PlayerStats>();
        float dropChance = enemyData.moneyDropChance;
        if (playerStats != null)
        {
            dropChance *= playerStats.Luck;
        }

        if (UnityEngine.Random.value <= dropChance)
        {
            Debug.Log("Money dropped");
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