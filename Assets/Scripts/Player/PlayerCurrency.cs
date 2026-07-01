using UnityEngine;
using System;

public class PlayerCurrency : MonoBehaviour
{
    [SerializeField] private int money;

    public event Action<int> OnMoneyChanged;

    public int Money => money;

    public void AddMoney(int amount)
    {
        if (amount <= 0)
            return;

        money += amount;

        OnMoneyChanged?.Invoke(money);

        Debug.Log($"Money: {money}");
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;

        OnMoneyChanged?.Invoke(money);

        return true;
    }
    public bool CanAfford(int amount)
    {
        return money >= amount;
    }
}