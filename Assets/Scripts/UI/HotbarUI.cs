using UnityEngine;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private HotbarSlotUI slotPrefab;
    [SerializeField] private Transform slotParent;
    private PlayerInventory inventory;

    private HotbarSlotUI[] slots;
    private int currentIndex = 0;
    public void Init(GameObject player)
    {
        inventory = player.GetComponentInChildren<PlayerInventory>();

        slots = new HotbarSlotUI[inventory.HotbarSlotCount];

        for (int i = 0; i < slots.Length; i++)
        {
            HotbarSlotUI slot = Instantiate(slotPrefab, slotParent);

            slot.SetKeyNumber(i + 1);

            slots[i] = slot;
        }

        inventory.OnInventoryChanged += RefreshIcons;

        RefreshSelection();
        RefreshIcons();
    }
    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSlot(i);
            }
        }
    }

    private void SetSlot(int index)
    {
        currentIndex = index;
        RefreshSelection();
    }

    public void SetSelected(int index)
    {
        currentIndex = index;
        RefreshSelection();
    }

    private void RefreshSelection()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SetSelected(i == currentIndex);
        }
    }
    private void RefreshIcons()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.WeaponSlotCount)
            {
                WeaponData weapon = inventory.GetWeapon(i);
                slots[i].SetIcon(weapon != null ? weapon.weaponSprite : null);
            }
            else
            {
                int itemIndex = i - inventory.WeaponSlotCount;

                ItemData item = inventory.GetItem(itemIndex);
                slots[i].SetIcon(item != null ? item.icon : null);
            }
        }
    }
    private void OnDestroy()
    {
        if (inventory != null)
            inventory.OnInventoryChanged -= RefreshIcons;
    }
}