using UnityEngine;

[System.Serializable]
public class StabPattern : AttackPattern
{
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float range = 2f;
    [SerializeField] private float width = 0.5f;
    [SerializeField] private float activeTime = 0.15f;

    public override void execute(HitContext context)
    {
        Vector2 spawnPos = context.origin + context.direction * (range * 0.5f);

        GameObject obj = Object.Instantiate(hitboxPrefab, spawnPos, Quaternion.identity);
        obj.transform.up = context.direction;
        obj.transform.localScale = new Vector3(width, range, 1f);
        
        obj.GetComponent<Hitbox>().activate(context);

        Object.Destroy(obj, activeTime);
    }
}