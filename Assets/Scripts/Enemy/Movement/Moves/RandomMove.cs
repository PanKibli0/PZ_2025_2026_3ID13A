using UnityEngine;

[System.Serializable]
public class RandomMove : IEnemyMove
{
    [SerializeField] private float changeInterval = 1f;

    private Vector2 currentDirection;
    private float timer;

    public void init(Transform player)
    {
        currentDirection = Random.insideUnitCircle.normalized;
    }

    public void onCollisionWithWall()
    {
        currentDirection = Random.insideUnitCircle.normalized;
        timer = 0;
    }

    public Vector2 getMovement(Vector2 currentPosition, float deltaTime, float speed)
    {
        timer += deltaTime;
        if (timer >= changeInterval)
        {
            timer = 0;
            currentDirection = Random.insideUnitCircle.normalized;
        }
        return currentDirection;
    }
}