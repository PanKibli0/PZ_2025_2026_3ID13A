using UnityEngine;

public class PlayerItemController : MonoBehaviour
{
    [SerializeField] private PlayerInventory inventory;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.UseItem(0, gameObject);
        }
    }
}