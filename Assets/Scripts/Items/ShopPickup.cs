using UnityEngine;

public class ShopPickup : MonoBehaviour
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private int price = 15;
    [SerializeField] private SpriteRenderer iconRenderer;
    private bool playerInside;
    private GameObject player;
    [SerializeField] private ShopItemUI shopItemUI;

    private void Start()
    {
        if (itemData != null && iconRenderer != null) 
        {
            iconRenderer.sprite = itemData.icon;   
        }
        shopItemUI.SetPrice(price);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = true;
        player = collision.gameObject;

        PlayerCurrency currency = player.GetComponentInChildren<PlayerCurrency>();

        shopItemUI.ShowAction(currency, price);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        playerInside = false;
        player = null;

        shopItemUI.HideAction();
    }
    private void Update()
    {
        if (!playerInside)
            return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryBuy();
        }
    }
    private void TryBuy()
    {
        PlayerInventory inventory = player.GetComponentInChildren<PlayerInventory>();
        if (inventory == null)
            return;

        PlayerCurrency currency = player.GetComponentInChildren<PlayerCurrency>();
        if (currency == null)
            return;

        if (!currency.CanAfford(price))
        {
            Debug.Log("Za ma³o pieniêdzy");
            return;
        }

        bool wasPickedUp = itemData.ApplyEffect(player);

        if (!wasPickedUp)
            return;

        currency.SpendMoney(price);
        shopItemUI.HideAction();
        EventBus.PublishItemBought();
        Destroy(gameObject);
    }
}
