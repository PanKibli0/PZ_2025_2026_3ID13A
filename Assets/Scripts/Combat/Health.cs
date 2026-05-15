using UnityEngine;
using System;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 10;
    // TODO: SO DAtaDriven

    [SerializeField] private int currentHealth;

    public Action<int, int> onHealthChanged;
    public Action onDeath;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void applyHit(HitData hitData)
    {
        Debug.Log($"Received hit with damage: {hitData.damage}");

        currentHealth -= hitData.damage;
        onHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"Current health after hit: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath?.Invoke();
        }
    }
}
