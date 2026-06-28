using UnityEngine;

public class EnemyUnit : Unit
{
    protected override void Death()
    {
        Destroy(gameObject);
    }
}
