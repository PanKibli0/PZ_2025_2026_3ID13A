using UnityEngine;

public interface IEnemyMove
{
    void init(Transform player) { }
    void onCollisionWithWall() { }
    Vector2 getMovement(Vector2 currentPosition, float deltaTime, float speed);
}