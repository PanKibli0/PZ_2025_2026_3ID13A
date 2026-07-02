using UnityEngine;

public class MoneyPickup : MonoBehaviour
{
    [SerializeField] private int amount = 1;

    public void Init(int value)
    {
        amount = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        PlayerCurrency currency = other.GetComponentInChildren<PlayerCurrency>();

        if (currency != null)
        {
            currency.AddMoney(amount);
        }

        Destroy(gameObject);
    }
}