using System;
using UnityEngine;

[Serializable]
public class Health
{
    public int maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public event Action<float, int> OnHealthChanged;
    public event Action OnDeath;

    public void Init(int max)
    {
        maxHealth = max;
        currentHealth = max;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void TakeDamage(float amount)
    {
        if (amount <= 0) return;
        
        PlayerStats playerStats = GetComponentInParent<PlayerStats>();

        if (playerStats != null)
        {
            if (UnityEngine.Random.value <= playerStats.DodgeChance / 100f)
                return;
        }

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            OnDeath?.Invoke();
    }

    public void TakeHeal(float amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        if (newMaxHealth <= 0)
            return;

        float difference = newMaxHealth - maxHealth;

        maxHealth = newMaxHealth;
        currentHealth += difference;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}