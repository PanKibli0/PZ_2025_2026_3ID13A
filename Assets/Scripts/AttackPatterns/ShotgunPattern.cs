using UnityEngine;

[System.Serializable]
public class ShotgunPattern : AttackPattern
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int count = 5;
    [SerializeField] private float spread = 45f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 3f;

    public override void execute(HitContext context)
    {
        for (int i = 0; i < count; i++)
        {
            float randomAngle = Random.Range(-spread * 0.5f, spread * 0.5f);
            Vector2 dir = Quaternion.Euler(0, 0, randomAngle) * context.direction;

            GameObject obj = Object.Instantiate(projectilePrefab, context.origin, Quaternion.identity);
            obj.transform.up = dir;
            obj.GetComponent<Projectile>().setup(speed, dir, lifetime, context.attackerFaction, context);
        }
    }
}
