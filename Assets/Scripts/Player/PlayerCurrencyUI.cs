using TMPro;
using UnityEngine;

public class PlayerCurrencyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    private PlayerCurrency playerCurrency;

    public void Init(PlayerCurrency currency)
    {
        if (playerCurrency != null)
            playerCurrency.OnMoneyChanged -= UpdateMoney;

        playerCurrency = currency;

        if (playerCurrency == null)
            return;

        playerCurrency.OnMoneyChanged += UpdateMoney;

        UpdateMoney(playerCurrency.Money);
    }

    private void UpdateMoney(int amount)
    {
        moneyText.text = $"${amount}";
    }

    private void OnDestroy()
    {
        if (playerCurrency != null)
            playerCurrency.OnMoneyChanged -= UpdateMoney;
    }
}