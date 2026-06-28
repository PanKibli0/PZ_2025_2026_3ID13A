using UnityEngine;

[System.Serializable]
public class TowardsPlayerMove : IEnemyMove
{
    private Transform player;

    public void init(Transform player)
    {
        this.player = player;
    }

    public Vector2 getMovement(Vector2 currentPosition, float deltaTime, float speed)
    {
        if (player == null) return Vector2.zero;
        return (player.position - (Vector3)currentPosition).normalized;
    }
}