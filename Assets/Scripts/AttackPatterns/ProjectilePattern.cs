using UnityEngine;

[System.Serializable]
public class ProjectilePattern : AttackPattern
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifetime = 5f;

    public override void execute(AttackContext context)
    {
        GameObject obj = Object.Instantiate(projectilePrefab, context.origin, Quaternion.identity);
        obj.transform.up = context.direction;

        obj.GetComponent<Hitbox>().activate(context);
        obj.GetComponent<Projectile>().setup(speed, context.direction, lifetime, context.attackerFaction);
    }
}
