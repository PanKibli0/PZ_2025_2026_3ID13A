using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public WeaponData[] weapons = new WeaponData[5];

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
}