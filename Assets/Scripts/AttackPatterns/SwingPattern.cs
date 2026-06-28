using UnityEngine;

[System.Serializable]
public class SwingPattern : AttackPattern
{
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float range = 1.5f;
    [SerializeField] private float width = 2f;
    [SerializeField] private float activeTime = 0.25f;

    public override void Execute(HitContext context)
    {
        Vector2 spawnPos = context.origin + context.direction * (range * 0.5f);

        GameObject obj = Object.Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        obj.transform.up = context.direction;
        obj.transform.localScale = new Vector3(width, 1f, 1f);

        obj.GetComponent<Hitbox>().Activate(context);
        Object.Destroy(obj, activeTime);
    }
}
