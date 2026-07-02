using System;
using static EnemyData;


public static class EventBus
{
    public static event Action OnAllEnemiesDefeated;

    public static event Action<UnityEngine.Vector2, UnityEngine.Vector2> OnRoomEntered;

    public static event Action OnLevelUp;

    public static event Action<EnemyType> OnEnemyKilled;

    public static event Action<int> OnMoneyCollected;

    public static event Action<int> OnLevelReached;

    public static event Action OnItemBought;

    public static event Action OnRoomCleared;

    public static void PublishRoomCleared()
    {
        OnRoomCleared?.Invoke();
    }

    public static void PublishMoneyCollected(int amount)
    {
        OnMoneyCollected?.Invoke(amount);
    }
    public static void PublishLevelReached(int level)
    {
        OnLevelReached?.Invoke(level);
    }
    public static void PublishItemBought()
    {
        OnItemBought?.Invoke();
    }

    public static void publishAllEnemiesDefeated()
    {
        OnAllEnemiesDefeated?.Invoke();
    }

    public static void publishOnRoomEntered(UnityEngine.Vector2 v1, UnityEngine.Vector2 v2)
    {
        OnRoomEntered?.Invoke(v1, v2);
    }

    public static void publishLevelUp()
    {
        OnLevelUp?.Invoke();
    }

    public static void PublishEnemyKilled(EnemyType enemyType)
    {
        OnEnemyKilled?.Invoke(enemyType);
    }
}