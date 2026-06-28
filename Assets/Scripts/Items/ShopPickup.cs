using UnityEngine;

public class ShopPickup : MonoBehaviour, IPickup
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

    public void OnPickup(PlayerInventory inventory)
    {
        if (itemData == null)
        {
            Debug.LogError("Brak ItemData w sklepie");
            return;
        }

        if (inventory.coins < price) return;

        bool wasPickedUp = itemData.ApplyEffect(inventory);

        if (wasPickedUp)
        {
            inventory.coins -= price;
            Debug.Log($"Kupiono {itemData.name}");
            Destroy(gameObject);
        }
    }
}
