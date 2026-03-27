using UnityEngine;

public class HitBox : MonoBehaviour
{
    [SerializeField]
    CombatControl owner;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HurtBox hb = collision.gameObject.GetComponent<HurtBox>();
        if (hb is not null)
        {
            if (hb.owner != owner)
            {
                hb.owner.SomethingPolymorphismHurt(owner.SomethingPolymorphismHit());
            }
        }
    }
}
