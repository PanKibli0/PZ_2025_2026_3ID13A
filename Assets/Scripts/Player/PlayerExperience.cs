using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    [SerializeField] private int currentLevel = 1;
    [SerializeField] private int currentXP = 0;
    [SerializeField] private int xpToNextLevel = 100;

    public void AddExperience(int amount)
    {
        Debug.Log($"AddExperience amount = {amount}");

        currentXP += amount;

        Debug.Log($"XP: {currentXP}/{xpToNextLevel}");

        while (currentXP >= xpToNextLevel)
        {
            currentXP -= xpToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;

        xpToNextLevel += 100;

        Debug.Log($"LEVEL UP! Level {currentLevel}");
        EventBus.publishLevelUp();
    }
}