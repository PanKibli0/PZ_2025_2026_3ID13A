using System;

public static class EventBus
{
    public static event Action OnAllEnemiesDefeated;

    public static void publishAllEnemiesDefeated()
    {
        OnAllEnemiesDefeated?.Invoke();
    }
}