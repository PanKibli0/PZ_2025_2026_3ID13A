using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponData[] weapons = new WeaponData[5];
    [SerializeField] private ItemData[] items = new ItemData[5];

    public int coins = 0;
    

    public bool AddWeapon(WeaponData weapon)
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                weapons[i] = weapon;
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
    }
    public bool AddItem(ItemData item)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = item;
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