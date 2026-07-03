using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HotbarSlotUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text keyNumber;

    public void SetKeyNumber(int number)
    {
        keyNumber.text = number.ToString();
    }

    public void SetIcon(Sprite icon)
    {
        if (icon == null)
        {
            itemIcon.enabled = false;
            return;
        }

        itemIcon.enabled = true;
        itemIcon.sprite = icon;
    }

    public void SetSelected(bool selected)
    {
        background.color = selected ? Color.yellow : Color.white;
    }
}