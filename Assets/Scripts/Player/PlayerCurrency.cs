using UnityEngine;
using System;

public class PlayerCurrency : MonoBehaviour
{
    [SerializeField] private int money;

    public event Action<int> OnCoinsChanged;

    public int Money => money;

    public void AddMoney(int amount)
    {
        if (amount <= 0)
            return;

        money += amount;

        OnCoinsChanged?.Invoke(money);

        Debug.Log($"Money: {money}");
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;

        OnCoinsChanged?.Invoke(money);

        return true;
    }
}