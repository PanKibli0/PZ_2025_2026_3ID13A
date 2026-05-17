using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;

    [SerializeField] private int currentHealth;

    public Action<int, int> onHealthChanged; // change to SO event for better decoupling
    public Action onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;
        onHealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke();
        }
    }

    public void takeHeal(int amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        onHealthChanged?.Invoke(currentHealth, maxHealth);
    }
}