using UnityEngine;
using System;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private UnitDataSO unitData;

    public int maxHealth;
    public int currentHealth;

    public Action<int, GameObject> OnDamaged;
    public Action<int, GameObject> OnHealed;
    public Action OnDeath;

    private void Awake()
    {
        if (unitData != null)
        {
            maxHealth = unitData.maxHealth;
            currentHealth = maxHealth;
        }
    }

    public void takeDamage(int damage, GameObject source)
    {
        currentHealth -= damage;
        OnDamaged?.Invoke(damage, source);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            OnDeath?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void heal(int amount, GameObject source)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        OnHealed?.Invoke(amount, source);
    }

    public void setMaxHealth(int newMax)
    {
        maxHealth = newMax;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }
}