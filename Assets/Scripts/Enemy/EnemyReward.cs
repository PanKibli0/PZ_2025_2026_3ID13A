using UnityEngine;

public class EnemyReward : MonoBehaviour
{
    private EnemyData enemyData;

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
    }
}