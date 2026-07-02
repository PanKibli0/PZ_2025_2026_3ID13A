using TMPro;
using UnityEngine;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] private TMP_Text priceText;
    [SerializeField] private TMP_Text actionText;
    private PlayerCurrency currency;
    private int currentPrice;

    private void Awake()
    {
        actionText.gameObject.SetActive(false);
    }

    public void SetPrice(int price)
    {
        priceText.text = $"${price}";
    }

    public void ShowAction(PlayerCurrency playerCurrency, int price)
    {
        currency = playerCurrency;
        currentPrice = price;

        actionText.gameObject.SetActive(true);

        currency.OnMoneyChanged += Refresh;

        Refresh(currency.Money);
    }
    private void Refresh(int money)
    {
        if (money >= currentPrice)
        {
            actionText.text = "[E] Buy";
            actionText.color = Color.green;
        }
        else
        {
            actionText.text = "Not enough money";
            actionText.color = Color.red;
        }
    }

    public void HideAction()
    {
        if (currency != null)
            currency.OnMoneyChanged -= Refresh;

        currency = null;

        actionText.gameObject.SetActive(false);
    }
}