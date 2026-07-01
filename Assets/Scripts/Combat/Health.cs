using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private int currentHealth;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        if (currentHealth <= 0) // Uszkodzony przeciwnik na start (??)
            currentHealth = maxHealth;
    }

    public void takeDamage(int amount)
    {
        if (amount <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Max(0, currentHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
            OnDeath?.Invoke();
        
    }

    public void takeHeal(int amount)
    {
        if (amount <= 0) return;

        currentHealth += amount;
        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void setMaxHealth(int newMaxHealth)
    {
        if (newMaxHealth <= 0)
            return;

        int difference = newMaxHealth - maxHealth;

        maxHealth = newMaxHealth;
        currentHealth += difference;

        currentHealth = Mathf.Min(currentHealth, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
}