using UnityEngine;

public class ShopPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int price = 15;
    [SerializeField] private SpriteRenderer iconRenderer;

    private void Start()
    {
        if (itemData != null && iconRenderer != null) 
        {
            iconRenderer.sprite = itemData.icon;   
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject player = collision.gameObject;

        PlayerInventory inventory = player.GetComponent<PlayerInventory>();
        if (inventory == null)
            return;

        PlayerCurrency currency = player.GetComponent<PlayerCurrency>();
        if (currency == null)
            return;

        if (itemData == null)
        {
            Debug.LogError("Brak ItemData w sklepie");
            return;
        }

        if (!currency.CanAfford(price))
        {
            Debug.Log("Za ma³o pieniêdzy");
            return;
        }

        bool wasPickedUp = itemData.ApplyEffect(player);

        if (!wasPickedUp)
            return;

        currency.SpendMoney(price);

        Debug.Log($"Kupiono {itemData.itemName}");

        Destroy(gameObject);
    }
}
