using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public WeaponData[] weapons = new WeaponData[5];
    [SerializeField] private ItemData[] items = new ItemData[5];
    public int WeaponSlotCount => weapons.Length;
    public int ItemSlotCount => items.Length;
    public int HotbarSlotCount => WeaponSlotCount + ItemSlotCount;
    public int coins = 0;

    public bool HasKey { get; private set; }

    public event Action OnInventoryChanged;

    public bool AddWeapon(WeaponData weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = weapon;
                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        return false;
    }

    public WeaponData GetWeapon(int index)
    {
        if (index < 0 || index >= weapons.Length)
            return null;

        return weapons[index];
    }

    public void ReplaceWeaponAt(int index, WeaponData weapon)
    {
        if (index < 0 || index >= weapons.Length)
            return;

        weapons[index] = weapon;
        OnInventoryChanged?.Invoke();
    }
    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;

                if (item is KeyItemData)
                    HasKey = true;

                OnInventoryChanged?.Invoke();
                return true;
            }
        }

        return false;
    }

    public ItemData GetItem(int index)
    {
        if (index < 0 || index >= items.Length)
            return null;

        return items[index];
    }
    public void RemoveItem(int index)
    {
        if (index < 0 || index >= items.Length)
            return;

        items[index] = null;
        OnInventoryChanged?.Invoke();
    }
    public bool UseItem(int index, GameObject target)
    {
        ItemData item = GetItem(index);

        if (item == null)
            return false;

        bool success = item.ApplyEffect(target);

        if (success)
            RemoveItem(index);

        return success;
    }

}