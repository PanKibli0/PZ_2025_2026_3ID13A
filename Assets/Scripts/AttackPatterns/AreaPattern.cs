using UnityEngine;

[System.Serializable]
public class AreaPattern : AttackPattern
{
    [SerializeField] private GameObject hitboxPrefab;
    [SerializeField] private float radius = 2f;
    [SerializeField] private float activeTime = 0.1f;

    public override void execute(HitContext context)
    {
        GameObject obj = Object.Instantiate(hitboxPrefab, context.origin, Quaternion.identity);
        float diameter = radius * 2f;
        obj.transform.localScale = new Vector3(diameter, diameter, 1f);
        
        obj.GetComponent<Hitbox>().activate(context);
        Object.Destroy(obj, activeTime);
    }
}