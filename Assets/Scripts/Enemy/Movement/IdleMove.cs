using UnityEngine;

[System.Serializable]
public class IdleMove : IEnemyMove
{
    public Vector2 getMovement(Vector2 currentPosition, float deltaTime, float speed)
    {
        return Vector2.zero;
    }
}