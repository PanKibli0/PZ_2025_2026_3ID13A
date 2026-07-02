using System;


public static class EventBus
{
    public static event Action OnAllEnemiesDefeated;

    public static event Action<UnityEngine.Vector2, UnityEngine.Vector2> OnRoomEntered;

    public static event Action OnLevelUp;

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
}