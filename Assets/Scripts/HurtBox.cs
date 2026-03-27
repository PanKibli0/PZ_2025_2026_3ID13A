using UnityEngine;

public class HurtBox : MonoBehaviour
{
    [SerializeField]
    private CombatControl p_owner;
    public CombatControl owner
    {
        get
        {
            return p_owner;
        }
    }
}
