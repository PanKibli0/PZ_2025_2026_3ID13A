using UnityEngine;

[System.Serializable]
public class ArcMove : IEnemyMove
{
    [Tooltip("Optional: If > 0, limits the arc to a specific length. Otherwise, it will be a full circle.")]
    [SerializeField] private float arcLength = 0f;

    private Transform player;
    private float radius;
    private float centerAngle;
    private float currentAngle;
    private float direction = 1f;
    private bool initialized;

    public void init(Transform player)
    {
        this.player = player;
        initialized = false;
        direction = 1f;
    }

    public void onCollisionWithWall()
    {
        direction = -direction;
    }

    public Vector2 getMovement(Vector2 currentPosition, float deltaTime, float speed)
    {
        if (player == null) return Vector2.zero;

        Vector2 playerPos = player.position;

        if (!initialized)
        {
            Vector2 offset = currentPosition - playerPos;
            radius = offset.magnitude;
            centerAngle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            currentAngle = centerAngle;
            initialized = true;
        }

        float angularSpeedDeg = (speed / radius) * Mathf.Rad2Deg;
        currentAngle += direction * angularSpeedDeg * deltaTime;

        float half = arcLength <= 0f ? 180f : (arcLength / radius) * Mathf.Rad2Deg / 2f;
        float deviation = Mathf.DeltaAngle(centerAngle, currentAngle);

        if (deviation > half)
        {
            currentAngle = centerAngle + half;
            direction = -1f;
        }
        else if (deviation < -half)
        {
            currentAngle = centerAngle - half;
            direction = 1f;
        }

        float rad = currentAngle * Mathf.Deg2Rad;
        Vector2 desiredPos = playerPos + new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * radius;

        return (desiredPos - currentPosition).normalized;
    }
}