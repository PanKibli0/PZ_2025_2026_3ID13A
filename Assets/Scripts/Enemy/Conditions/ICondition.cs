using UnityEngine;

public interface ICondition
{
    bool isMet(GameObject enemy, Transform player, Health health);
}