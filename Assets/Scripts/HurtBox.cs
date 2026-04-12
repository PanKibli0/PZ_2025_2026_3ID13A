using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    [SerializeField] private HealthComponent healthComponent;

    public void takeDamage(int damage, GameObject source)
    {
        if (healthComponent != null)
            healthComponent.takeDamage(damage, source);
    }

    public void heal(int amount, GameObject source)
    {
        if (healthComponent != null)
            healthComponent.heal(amount, source);
    }
}