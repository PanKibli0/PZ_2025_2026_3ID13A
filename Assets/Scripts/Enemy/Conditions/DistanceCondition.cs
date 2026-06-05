using UnityEngine;

[System.Serializable]
public class DistanceCondition : ICondition
{
    [SerializeField] private float minDistance = 0f;
    [SerializeField] private float maxDistance = float.MaxValue;

    public bool isMet(GameObject enemy, Transform player, Health health)
    {
        if (player == null) return false;

        float distance = Vector2.Distance(enemy.transform.position, player.position);

        return distance >= minDistance && distance <= maxDistance;
    }
}